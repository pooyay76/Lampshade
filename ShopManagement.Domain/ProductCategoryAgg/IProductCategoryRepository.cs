using Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchModel);
        public List<ProductViewModel> GetProducts(long id);
        public List<ProductCategoryMinimalViewModel> ListProductCategoryWithProducts();
    }
}
