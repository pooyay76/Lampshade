﻿using LampshadeQuery.Contracts.ProductCategoryAgg;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoriesViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;

        public ProductCategoriesViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }
        public IViewComponentResult Invoke()
        {
            var trash = productCategoryQuery.GetProductCategoriesWithProductsDetails();
            return View(productCategoryQuery.GetProductCategories());
        }
    } 
}
