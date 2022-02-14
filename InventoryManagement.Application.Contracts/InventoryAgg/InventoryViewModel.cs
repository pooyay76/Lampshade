
namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class InventoryViewModel
    {
        public long Id{ get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public long UnitPrice { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }

    }


}
