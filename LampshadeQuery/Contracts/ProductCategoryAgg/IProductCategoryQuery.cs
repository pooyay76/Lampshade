using LampshadeQuery.Contracts.ProductAgg;
using System.Collections.Generic;

namespace LampshadeQuery.Contracts.ProductCategoryAgg
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
        public ProductCategoryQueryModel GetProductCategoryById(long id);
        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();

    }
}
