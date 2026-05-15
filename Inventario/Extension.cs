using Inventario.Entities;
using static Inventario.Dtos;

namespace Inventario
{
    public static class Extension
    {
        public static InventoryItemDto AsDto(this InventoryItem item, String Name, String Description)
        {
            return new InventoryItemDto(item.CatalogItemId, Name, Description, item.Quantity, item.AcquiredDate);
        }
    }
}
