using Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,ProductViewModel>
    {
        public List<ProductViewModel> Search(ProductSearchModel query);
        public ProductViewModel GetProductCategory(long id);
        public List<ProductViewModel> ListProductsWithCategories();
    }
}
