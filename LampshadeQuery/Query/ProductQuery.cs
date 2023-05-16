using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using LampshadeQuery.Contracts.Product;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using System;
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
            //LIST OF DEFINED PRICES OF EACH PRODUCT
            var prices = inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice });

            //LIST OF ACTIVE DISCOUNTS
            var discounts = discountContext.CustomerDiscounts
                .Where(x => x.EndDate > DateTime.Now && x.StartDate <= DateTime.Now)
                .Select(x => new { EndDate = x.EndDate, DiscountPercentage = x.DiscountPercentage, ProductId = x.ProductId })
                .AsNoTracking();

            //LIST OF ALL PRODUCTS
            var products = shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel
            {
                Slug = x.Slug,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug,
                Name = x.Name,
                Id = x.Id
            }).AsNoTracking().ToList();

            //ITERATING TO JOIN
            foreach (var product in products)
            {
                var price = prices.FirstOrDefault(x => x.ProductId == product.Id);
                product.Price = (price == null ? 0 : price.UnitPrice);
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                product.DiscountPercentage = discount == null ? 0 : discount.DiscountPercentage;
            }

            return products.OrderByDescending(x=>x.Id).Take(12).ToList();
        }

    }
}
