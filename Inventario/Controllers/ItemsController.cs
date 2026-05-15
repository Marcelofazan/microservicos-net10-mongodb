using Inventario.Clients;
using Inventario.Entities;
using Microsoft.AspNetCore.Mvc;
using Pacote;
using static Inventario.Dtos;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly CatalogClient catalogClient;
        private readonly IRepository<InventoryItem> itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository, CatalogClient catalogClient)
        {
            this.itemsRepository = itemsRepository;
            this.catalogClient = catalogClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty) { return BadRequest("UserId is required."); }

            var catalogItems = await catalogClient.GetCatalogItemsAsync();

            var items = (await itemsRepository.GetAllAsync()).Where(item => item.UserId == userId);

            var inventoryItemDtos = items.Select(item =>
            {
                var catalogItem = catalogItems.Single(catalogItem => catalogItem.Id == item.CatalogItemId);
                return item.AsDto(catalogItem.Name, catalogItem.Description);
            });
            return Ok(inventoryItemDtos);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> PostAsync(GrantItemsDto grantItemsDto)
        {
            var inventoryItem = await itemsRepository.GetAsync(grantItemsDto.UserId);
            if (inventoryItem is null)
            {
                inventoryItem = new InventoryItem
                {
                    UserId = grantItemsDto.UserId,
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };
                await itemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemsDto.Quantity;
                await itemsRepository.UpdateAsync(inventoryItem);
            }
            return Ok();
        }
    }
}
