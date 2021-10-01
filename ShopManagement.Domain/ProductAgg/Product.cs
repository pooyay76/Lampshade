using Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Domain.ProductAgg
{
    public class ProductViewModel : EntityBase
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Name { get; private set; }


        [Range(0, (float)(decimal.MaxValue),ErrorMessage = ValidationMessages.NotInRangeMessage)]
        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        public decimal UnitPrice { get; private set; }


        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        public bool IsInStock { get; private set; }

        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        public string Code { get; private set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Description { get; private set; }


        [Required(ErrorMessage = ValidationMessages.RequiredMessage,AllowEmptyStrings =false)]
        public string Picture { get; private set; }

        //Product Category
        [Range(0,long.MaxValue,ErrorMessage =ValidationMessages.NotInRangeMessage)]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }

        //Product Pictures
        public List<ProductPicture> ProductPictures { get; private set; }


        //SEO
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Slug { get; private set; }

        public string ShortDescription { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }


        public ProductViewModel(string name, decimal unitPrice,string description, bool isInStock,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            string slug, long categoryId, string code, string shortDescription)
        {
            Name = name;
            UnitPrice = unitPrice;
            IsInStock = true;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Code = code;
            IsInStock = isInStock;

        }
        public void Edit(string name, decimal unitPrice, bool isInStock, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            string slug, long categoryId,string code,string shortDescription)
        {
            Name = name;
            UnitPrice = unitPrice;
            IsInStock = isInStock;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Code = code;
        }
        public void InStock()
        {
            IsInStock = true;
        }
        public void NotInStock()
        {
            IsInStock = false;
        }
    }


}
