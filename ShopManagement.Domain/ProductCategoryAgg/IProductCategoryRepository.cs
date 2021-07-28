using Framework.Domain;
using ShopManagement.Application.Contracts;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        public long Count();
        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchModel);
    }
}
