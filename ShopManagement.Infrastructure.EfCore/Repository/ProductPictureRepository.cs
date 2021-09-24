using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            this.context = context;
        }

        public ProductPicture GetProductPicture(long id)
        {
            return context.ProductPictures.Include(x => x.Product).FirstOrDefault(x => x.Id == id);

        }

        public List<ProductPictureMinimalViewModel> ListProductPicturesWithProducts()
        {
            return context.ProductPictures.AsNoTracking()
                .Include(x=>x.Product)
                .Select(x => new ProductPictureMinimalViewModel() 
                {
                Id = x.Id,
                Picture=x.Picture,
                ProductName = x.Product.Name
                })
                .OrderByDescending(x=>x.Id)
                .ToList();

        }


        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture query)
        {

            var items = context.ProductPictures.AsNoTracking()
                .Include(x=>x.Product)
                .Select(x=>new ProductPictureMinimalViewModel()
                {
                    ProductName = x.Product.Name,
                    Picture = x.Picture,
                    Id = x.Id
                });
            if (!string.IsNullOrWhiteSpace(query.ProductName))
                items = items.Where(x => x.ProductName.Contains(query.ProductName));
            return items
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}
