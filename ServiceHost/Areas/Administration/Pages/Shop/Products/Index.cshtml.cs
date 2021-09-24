using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication productApplication;
        private readonly IProductCategoryApplication productCategoryApplication;
        public List<ProductMinimalViewModel> Items { get; set; }
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            this.productApplication = productApplication;
            this.productCategoryApplication = productCategoryApplication;
        }

        public void OnGet()
        {
            Items = productApplication.List();
        }
        public PartialViewResult OnGetCreate()
        {
            var Form = new CreateProduct
            {
                Categories = productCategoryApplication.List()
            };
            return Partial("./Create",Form);
        }
        public JsonResult OnPostCreate(CreateProduct form)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(productApplication.Create(form));
            }
            else
                return new JsonResult((new OperationResult()).Failed());
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = productApplication.EditGet(id);
            item.Categories = productCategoryApplication.List();
            if(item != null)
                return Partial("./Edit",item);
            return null;

        }
        public JsonResult OnPostEdit(EditProduct form)
        {
            if(ModelState.IsValid)
            {
                return new JsonResult(productApplication.Edit(form));
            }
            return new JsonResult((new OperationResult()).Failed("Validation Error"));
        }
    }
}
