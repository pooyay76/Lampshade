using LampshadeQuery.Contracts.ProductAgg;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            this.productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View(productQuery.GetLatestArrivals());
        }
    }
}
