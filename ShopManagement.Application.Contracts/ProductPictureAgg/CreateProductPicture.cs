using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public class CreateProductPicture
    {
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long ProductId { get; set; }
        public List<ProductMinimalViewModel> Products { get; set; }
    }
}
