using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository :RepositoryBase<long,Product>, IProductRepository
    {
        private readonly ShopContext shopContext;
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            this.shopContext = shopContext;
        }

        public Product GetProductCategory(long id)
        {
            return shopContext.Products.Include(x => x.Category).FirstOrDefault(x=>id == x.Id);

        }

        public List<Product> ListProductsWithCategories()
        {
            return shopContext.Products.Include(x => x.Category).ToList();
        }

        public List<Product> Search(ProductSearchModel query)
        {
            var result = shopContext.Products.Include(x => x.Category).AsNoTracking().AsQueryable();
            if (query.Name!= null)
                result = result.Where(x => x.Name == query.Name);
            if (query.UnitPrice != 0)
                result = result.Where(x => x.UnitPrice == query.UnitPrice);
            if (query.CategoryName != null)
                result = result.Where(x => x.Category.Name == query.CategoryName);
            if (query.Code != null)
                result = result.Where(x => x.Code == query.Code);
            if (query.Description != null)
                result = result.Where(x => x.Description == query.Description);
            return result.ToList();

        }


    }
}
