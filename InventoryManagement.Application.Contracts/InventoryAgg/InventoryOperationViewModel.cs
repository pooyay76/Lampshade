using System;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class InventoryOperationViewModel
    {
        public long Id { get; set; }
        public bool IsSold { get; set; }
        public long Count { get; set; }
        public long OperatorId { get; set; }
        public string OperatorName { get; set; }
        public DateTime OperationDateTime { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public int CountBeforeOperation { get; set; }
    }
}
