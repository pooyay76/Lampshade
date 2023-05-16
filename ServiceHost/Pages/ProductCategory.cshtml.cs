using LampshadeQuery.Contracts.Product;
using LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery productCategoryQuery;
        public List<ProductQueryModel> Products { get; set; }
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(long id)
        {
            Products = productCategoryQuery.GetProductCategoryWithProducts(id).Products;
        }
    }
}
