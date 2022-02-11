using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
