using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public interface IProductApplication
    {
        public OperationResult Create(CreateProduct dataEntry);
        public OperationResult Edit(EditProduct dataEntry);
        public ProductViewModel Get(long id);
        public EditProduct EditGet(long id);
        public OperationResult MakeInStock(long id);
        public OperationResult MakeNotInStock(long id);
        public List<ProductMinimalViewModel> List();
        public List<ProductMinimalViewModel> Search(ProductSearchModel query);
    }
}
