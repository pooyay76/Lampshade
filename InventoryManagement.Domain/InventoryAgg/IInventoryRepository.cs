using Framework.Application;
using Framework.Domain;
using InventoryManagement.Application.Contracts.InventoryAgg;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        public List<InventoryViewModel> Search(InventorySearchModel inventorySearchModel);
        public OperationResult Increase(long inventoryId, long operatorId,int count, string description);
        public OperationResult Decrease(long inventoryId, long operatorId, long orderId, int count, string description);
        public InventoryViewModel GetInventoryWithProduct(long inventoryId);
        public List<InventoryOperationViewModel> GetInventoryLog(long invetoryId);
    }
}
