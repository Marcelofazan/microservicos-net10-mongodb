using Catalogo.Entities;
using Microsoft.AspNetCore.Mvc;
using Catalogo.Dtos;
using Pacote;

namespace Catalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {

        private readonly IRepository<Item> itemRepository;
        public ItemsController(IRepository<Item> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}", Name = "GetByIdAsync")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await itemRepository.CreateAsync(item);
            var itemDto = item.AsDto();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDto.Id }, itemDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await itemRepository.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;
            await itemRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await itemRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
