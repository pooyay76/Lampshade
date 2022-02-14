using Framework.Application;
using Framework.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext inventoryContext;
        private readonly ShopContext shopContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) :base(inventoryContext)
        {
            this.inventoryContext = inventoryContext;
            this.shopContext = shopContext;
        }

        public OperationResult Decrease(long inventoryId,long operatorId, long orderId, int count, string description)
        {
            OperationResult operation = new();
            var data = inventoryContext.Inventories.FirstOrDefault(x => x.Id == inventoryId);
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Decrease(count,orderId, operatorId, description);
            inventoryContext.SaveChanges();
            return operation.Succeeded();
            
        }

        public OperationResult Increase(long inventoryId,long operatorId, int count, string description)
        {
            OperationResult operation = new();
            var data = inventoryContext.Inventories.FirstOrDefault(x => x.Id == inventoryId);
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Increase(count, operatorId, description);
            inventoryContext.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel inventorySearchModel)
        {
            var query = inventoryContext.Inventories;
            if (inventorySearchModel.ProductId != default)
                query.Where(x => x.ProductId == inventorySearchModel.ProductId);

            query.Where(x => x.IsInStock == inventorySearchModel.IsInStock);

            query.OrderByDescending(x => x.Id);


            var products = shopContext.Products.Select(x => new { x.Id, x.Name });
            var result = from item in query.AsEnumerable()
                         join product in products.AsEnumerable() on item.ProductId equals product.Id
                         select new InventoryViewModel()
                         {
                             Id = item.Id,
                             ProductId = item.ProductId,
                             Count = item.CurrentCount,
                             IsInStock = item.IsInStock,
                             UnitPrice = item.UnitPrice,
                             ProductName = product.Name
                         };


            return result.ToList();
        }
        public InventoryViewModel GetInventoryWithProduct(long id)
        {
            var data = inventoryContext.Inventories.FirstOrDefault(x => x.Id == id);
            var product = shopContext.Products.FirstOrDefault(x => x.Id == data.ProductId);
            return new InventoryViewModel() { 

                Count = data.CurrentCount, 
                ProductId = data.ProductId, 
                Id = data.Id, 
                IsInStock = data.IsInStock, 
                ProductName = product.Name, 
                UnitPrice = data.UnitPrice 
            };
        }

        public List<InventoryOperationViewModel> GetInventoryLog(long invetoryId)
        {
            Inventory data = Get(invetoryId);
            if (data == null)
                return null;
            return data.InventoryOperations.Select(x => new InventoryOperationViewModel() 
            { 
                Count = x.Count, 
                CountBeforeOperation = x.CountBeforeOperation, 
                Description = x.Description, 
                Id = x.Id, 
                IsSold = x.IsSold, 
                OperationDateTime = x.OpeartionDateTime,
                OperatorId = x.OperatorId, OperatorName = "مدیر",
                OrderId = x.OrderId }).ToList();
        }
    }
}
