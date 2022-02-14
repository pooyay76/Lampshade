using Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            OperationResult operation = new();
            Inventory entity = new(command.ProductId, command.UnitPrice);
            if (inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            inventoryRepository.Create(entity);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            OperationResult operation = new();
            var data = inventoryRepository.Get(command.Id);
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Edit(command.ProductId, command.UnitPrice);
            return operation.Succeeded();

        }

        public EditInventory EditGet(long id)
        {
            var data = inventoryRepository.Get(id);
            if (data == null)
                return null;
            return new EditInventory() { UnitPrice = data.UnitPrice, ProductId = data.ProductId, Id = id };
        }

        public OperationResult Increase(IncreaseInventory command)
        {
                return inventoryRepository.Increase(command.InventoryId, command.OperatorId, command.Count, command.Description);
        }
        public OperationResult Decrease(DecreaseInventory command)
        {
            return inventoryRepository.Decrease(command.InventoryId, command.OperatorId, command.OrderId, command.Count, command.Description);
        }
        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            OperationResult operation = new();

            foreach (var item in command)
            {
                if (!inventoryRepository.Exists(x => x.ProductId == item.ProductId))
                    return operation.Failed(ApplicationMessages.NotFoundMessage);
                Decrease(item);
            }
            return operation.Succeeded();
        }


        public List<InventoryViewModel> Search(InventorySearchModel command)
        {
            return inventoryRepository.Search(command);
        }

        public InventoryViewModel Get(long id)
        {
            return inventoryRepository.GetInventoryWithProduct(id);
        }

        public List<InventoryOperationViewModel> GetInventoryLog(long inventoryId)
        {
            return inventoryRepository.GetInventoryLog(inventoryId);
        }
    }
}