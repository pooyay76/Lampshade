using System.Collections.Generic;

namespace LampshadeQuery.Contracts.ProductCategoryAgg
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
        public List<ProductCategoryQueryModel> SearchProductCategories();
        public ProductCategoryQueryModel GetProductCategoryById(long id);
    }
}
