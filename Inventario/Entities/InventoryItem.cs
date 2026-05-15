using System.Security.Principal;
using Pacote;

namespace Inventario.Entities
{
    public class InventoryItem : IEntity
    {
        public Guid Id { get; init; }
        public Guid UserId { get; set; }
        public Guid CatalogItemId { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset AcquiredDate { get; set; }
    }
}
