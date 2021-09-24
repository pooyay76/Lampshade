using Framework.Domain;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long,ProductPicture>
    {
        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture query);
        public ProductPicture GetProductPicture(long id);
        public List<ProductPictureMinimalViewModel> ListProductPicturesWithProducts();
    }
}
