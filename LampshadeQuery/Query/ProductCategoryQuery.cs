using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using LampshadeQuery.Contracts.Product;
using LampshadeQuery.Contracts.ProductCategory;
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

        // BELOW METHOD RETURNS { Category1{
        //              Name,                        
        //              Product1{Price: a, Discount: x},
        //              Product2{Price:b,Discount:y}
        //              },
        //          Category2 {...},
        //           ...
        //          }
        public List<ProductCategoryQueryModel> GetProductCategoriesWithProductsDetails()
        {
            //LIST OF PRICES FOR EACH PRODUCT IN INVENTORY
            var prices = inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice });

            //LIST OF ACTIVE DISCOUNTS
            var discounts = discountContext.CustomerDiscounts
                .Where(x => x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now)
                .Select(x => new { EndDate = x.EndDate, DiscountPercentage = x.DiscountPercentage, ProductId = x.ProductId })
                .AsNoTracking();


            //GET PRODUCT CATEGORIES INLCUDED WITH PRODUCTS
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

            //joining discount and price for each product in each category using LINQ
            foreach (var category in categoriesWithProducts)
            {
                //foreach category we have many products which might have discounts and must have prices
                foreach (var product in category.Products)
                {
                    var price = prices.FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice;
                    product.Price = price ?? 0;

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    product.DiscountPercentage = (discount == null ? 0 : discount.DiscountPercentage);
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

        public ProductCategoryQueryModel GetProductCategoryWithProducts(long id)
        {
            var category = shopContext.ProductCategories.Include(x=>x.Products).FirstOrDefault(x => x.Id == id);
            if (category == null)
                return null;
            return new ProductCategoryQueryModel()
            { 
            Id = category.Id,
            Name = category.Name,
            Picture = category.Picture,
            PictureAlt = category.PictureAlt,
            PictureTitle = category.PictureTitle,
            Slug = category.Slug,
            Products = category.Products.Select(x=>new ProductQueryModel { Id = x.Id, Name = x.Name, Picture = x.Picture,Slug = x.Slug }).ToList()
            };

        }
    }
}
