using Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        public List<Product> Search(ProductSearchModel query);
        public Product GetProductCategory(long id);
        public List<Product> ListProductsWithCategories();
    }
}
