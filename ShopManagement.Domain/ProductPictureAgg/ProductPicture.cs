using Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture:EntityBase
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public ProductViewModel Product { get; private set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long ProductId { get; private set; }

        public ProductPicture(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }
        public void Edit(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }
    }

}
