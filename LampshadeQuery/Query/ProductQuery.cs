using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using LampshadeQuery.Contracts.ProductAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext shopContext;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.shopContext = shopContext;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var products = shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel
            {
                Slug = x.Slug,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Id = x.Id
            }).AsNoTracking();

            var discounts = discountContext.CustomerDiscounts.FirstOrDefault(x=>x.Id == )
        }

    }
}
