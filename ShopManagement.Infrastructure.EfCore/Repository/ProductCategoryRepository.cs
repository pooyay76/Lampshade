using System.Linq;
using ShopManagement.Domain.ProductCategoryAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using ShopManagement.Application.Contracts.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            this.context = context;
        }

        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchQuery)
        {
                var query = context.ProductCategories.Select(x => new ProductCategoryMinimalViewModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture
                });
            if (searchQuery != null)
            {
                query.Where(x => x.Name.Contains(searchQuery.Name));
            }
                return query.OrderByDescending(x => x.Id).ToList(); 
        }

        }
    }
