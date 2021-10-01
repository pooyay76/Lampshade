using System.Linq;
using ShopManagement.Domain.ProductCategoryAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductAgg;
using Microsoft.EntityFrameworkCore;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext shopContext;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            this.shopContext = context;
        }

        public List<ProductViewModel> GetProducts(long id)
        {
            var target = shopContext.ProductCategories.FirstOrDefault(x => x.Id == id);
            if (target == null) 
                return null;
            shopContext.Entry(target).Collection(x => x.Products).Load();
            return target?.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                Keywords = x.Keywords,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductCategoryViewModel> ListProductCategoryWithProducts()
        {
            shopContext.ProductCategories.Include(x => x.Products)
                .Select(x => 
                    {
                        List<string> productsNames = new();
                        var result = new ProductCategoryViewModel()
                        {
                            Description = x.Description,
                            Id = x.Id,
                            Name = x.Name,
                            Picture = x.Picture,
                            CreationDate = x.CreationDateTime.ToString(),
                        };
                    })
                .ToList();
        }

        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchQuery)
        {
                var query = shopContext.ProductCategories.Select(x => new ProductCategoryMinimalViewModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                });
            if (searchQuery != null)
            {
                query.Where(x => x.Name.Contains(searchQuery.Name));
            }
                return query.OrderByDescending(x => x.Id).ToList(); 
        }

        }
    }
