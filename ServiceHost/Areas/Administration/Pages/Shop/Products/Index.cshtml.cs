using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ProductSearchModel SearchModel { get; set; }
        public SelectList ProductCategories { get; set; }

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            this.productApplication = productApplication;
            this.productCategoryApplication = productCategoryApplication;
        }


        public void OnGet(ProductSearchModel command)
        {
            SearchModel = command;
            Items = productApplication.Search(command);
            ProductCategories = new SelectList(productCategoryApplication.List(),"Id","Name");
        }
        public PartialViewResult OnGetCreate()
        {
            var Form = new CreateProduct
            {
                Categories = productCategoryApplication.List()
            };
            return Partial("./Create",Form);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = productApplication.EditGet(id);
            item.Categories = productCategoryApplication.List();
            if(item != null)
                return Partial("./Edit",item);
            return null;

        }
        public JsonResult OnPostCreate(CreateProduct form)
        {
            if (ModelState.IsValid)
                return new JsonResult(productApplication.Create(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public JsonResult OnPostEdit(EditProduct form)
        {
            if(ModelState.IsValid)
                return new JsonResult(productApplication.Edit(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
    }
}
