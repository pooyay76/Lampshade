using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication productCategoryApplication;
        public List<ProductCategoryMinimalViewModel> Items { get; set; }

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            this.productCategoryApplication = productCategoryApplication;
        }

        public void OnGet()
        {
            Items = productCategoryApplication.List();
        }
        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create",new CreateProductCategory());
        }
        public JsonResult OnPostCreate(CreateProductCategory form)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(productCategoryApplication.Create(form));
            }
            else
                return new JsonResult((new OperationResult()).Failed());
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = productCategoryApplication.EditGet(id);
            if(item != null)
                return Partial("./Edit",item);
            return null;

        }
        public JsonResult OnPostEdit(EditProductCategory form)
        {
            if(ModelState.IsValid)
            {
                return new JsonResult(productCategoryApplication.Edit(form));
            }
            return new JsonResult((new OperationResult()).Failed("Validation"));
        }
    }
}
