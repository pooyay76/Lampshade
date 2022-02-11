using Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public class CreateProductPicture
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long ProductId { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<ProductMinimalViewModel> Products { get; set; }
    }
}
