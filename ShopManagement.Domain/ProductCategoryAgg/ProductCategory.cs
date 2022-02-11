using Framework.Application;
using Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory:EntityBase
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Name { get; private set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Description { get; private set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductCategory(string name, string description,string picture, string pictureAlt, 
            string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            Products = new();
        }
        public void Edit(string name, string description, string picture, string pictureAlt,
            string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
