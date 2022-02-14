using System.Collections.Generic;
using Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication productApplication;


        private readonly IInventoryApplication inventoryApplication;
        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Items { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            this.inventoryApplication = inventoryApplication;
            this.productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel inventorySearchModel)
        {
            Items = inventoryApplication.Search(inventorySearchModel);
            Products = new SelectList(productApplication.List(),"Id","Name");
        }

        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateInventory() { Products = new SelectList(productApplication.List(),"Id","Name")  });
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var data = inventoryApplication.EditGet(id);
            data.Products = new SelectList(productApplication.List(), "Id", "Name");
            if (data == null)
                return null;

            return Partial("./Edit", data);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            OperationResult operation = new();
            if (ModelState.IsValid)
            {
                operation = inventoryApplication.Create(command);
                return new JsonResult(operation);
            }
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            OperationResult operation = new();
            if (ModelState.IsValid)
            {
                operation = inventoryApplication.Edit(command);
                return new JsonResult(operation);
            }
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }


        public PartialViewResult OnGetIncrease(long id)
        {
            var data = inventoryApplication.Get(id);
            if (data == null)
                return null;
            const long operatorId = 1;
            return Partial("./Increase", new IncreaseInventory() { InventoryId=data.Id,OperatorId=operatorId});
        }

        public PartialViewResult OnGetDecrease(long id)
        {
            var data = inventoryApplication.Get(id);
            if (data == null)
                return null;
            const long operatorId = 1;
            return Partial("./Decrease", new DecreaseInventory() { InventoryId = data.Id,OperatorId=operatorId});
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            if (ModelState.IsValid)
                return new JsonResult(inventoryApplication.Increase(command));
            return new JsonResult(new OperationResult().Failed(ValidationMessages.InvalidModelStateMessage));
        }

        public JsonResult OnPostDecrease(DecreaseInventory command)
        {
            if (ModelState.IsValid)
                return new JsonResult(inventoryApplication.Decrease(command));
            return new JsonResult(new OperationResult().Failed(ValidationMessages.InvalidModelStateMessage));
        }


        public PartialViewResult OnGetReport(long id)
        {
           
            return Partial("./Report", inventoryApplication.GetInventoryLog(id));
        }
    }
}
