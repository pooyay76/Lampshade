using LampshadeQuery.Contracts.ProductAgg;
using System.Collections.Generic;

namespace LampshadeQuery.Contracts.ProductCategoryAgg
{
    public class ProductCategoryQueryModel
    {

        public string Name { get;  set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public List<ProductQueryModel> Products { get; set; }
        public long Id { get; set; }
    }
}
