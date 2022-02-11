using Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Description { get; set; }
        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        public bool IsInStock { get; set; }

        [Range(0, (float)(decimal.MaxValue), ErrorMessage = ValidationMessages.NotInRangeMessage)]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Picture { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.RequiredMessage)]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Slug { get; set; }

        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<ProductCategoryMinimalViewModel> Categories { get; set; }
    }
}
