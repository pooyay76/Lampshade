using Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(DecreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        EditInventory EditGet(long id);
        List<InventoryViewModel> Search(InventorySearchModel command);
        InventoryViewModel Get(long id);
        List<InventoryOperationViewModel> GetInventoryLog(long inventoryId);
        
    }
}
