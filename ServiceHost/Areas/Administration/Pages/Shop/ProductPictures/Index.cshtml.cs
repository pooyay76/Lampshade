using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication productApplication;
        private readonly IProductPictureApplication productPictureApplication;
        public List<ProductPictureMinimalViewModel> Items { get; set; }

        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            this.productApplication = productApplication;
            this.productPictureApplication = productPictureApplication;
        }

        public void OnGet()
        {
            Items = productPictureApplication.List();
        }
        public PartialViewResult OnGetCreate()
        {
            var Form = new CreateProductPicture()
            {
                Products = productApplication.List()
            };
            return Partial("./Create",Form);
        }
        public JsonResult OnPostCreate(CreateProductPicture form)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(productPictureApplication.Create(form));
            }
            else
                return new JsonResult((new OperationResult()).Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = productPictureApplication.EditGet(id);
            item.Products = productApplication.List();
            if(item != null)
                return Partial("./Edit",item);
            return null;

        }
        public JsonResult OnPostEdit(EditProductPicture form)
        {
            if(ModelState.IsValid)
            {
                return new JsonResult(productPictureApplication.Edit(form));
            }
            return new JsonResult((new OperationResult()).Failed(ValidationMessages.InvalidModelStateMessage));
        }
    }
}
