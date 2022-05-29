using _0_Framework.Application;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using LampshadeQuery.Contracts.ProductAgg;
using LampshadeQuery.Contracts.ProductCategoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext shopContext;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.shopContext = shopContext;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }

        // returns { Category1{
        //              Name,                        
        //              Product1{Price: a, Discount: x},
        //              Product2{Price:b,Discount:y}
        //              },
        //          Category2 {...},
        //           ...
        //          }
        public List<ProductCategoryQueryModel> GetProductCategoriesWithProductsDetails()
        {

            var prices = inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice });

            // get discount list
            var discounts = discountContext.CustomerDiscounts
                .Where(x => x.EndDate > DateTime.Now && x.StartDate <= DateTime.Now)
                .Select(x => new { EndDate = x.EndDate, DiscountRate = x.DiscountPercentage, ProductId = x.ProductId })
                .AsNoTracking();


            //get product categories with products
            var categoriesWithProducts = shopContext.ProductCategories.Include(x => x.Products).ThenInclude(x=>x.Category).AsNoTracking().Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Products = MapProducts(x.Products),
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();

            //joining discount and price for each category's products using LINQ
            foreach (var category in categoriesWithProducts)
            {
                //foreach category we have many products which might have discounts and must have prices
                foreach(var product in category.Products)
                {
                    product.Price = prices.FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice.ToMoney();
                    product.DiscountRate = discounts.FirstOrDefault(x => x.ProductId == product.Id)?.DiscountRate;
                }

            }

            return categoriesWithProducts.ToList();
            }

        private static List<ProductQueryModel> MapProductsMinimally(List<Product> products)
        {
            if (products == null)
                return null;

            return products.Select(x => new ProductQueryModel
            {
                Slug = x.Slug,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                Name = x.Name,
                Id = x.Id
            }).ToList();

        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            if (products == null)
                return null;

            return products.Select(x => new ProductQueryModel
            {
                Slug = x.Slug,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Id=x.Id
            }).ToList();

        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return shopContext.ProductCategories.Select(x=>new ProductCategoryQueryModel {
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryById(long id)
        {
            var item = shopContext.ProductCategories.FirstOrDefault(x => x.Id == id);
            return new ProductCategoryQueryModel
            {
                Name = item.Name,
                Picture = item.Picture,
                PictureAlt = item.PictureAlt,
                PictureTitle = item.PictureTitle,
                Slug = item.Slug
            };
        }

    }
}
