﻿using LampshadeQuery.Contracts.ProductCategoryAgg;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext shopContext;

        public ProductCategoryQuery(ShopContext shopContext)
        {
            this.shopContext = shopContext;
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

        public List<ProductCategoryQueryModel> SearchProductCategories()
        {
            throw new System.NotImplementedException();
        }
    }
}