using _0_Framework.Application;
namespace LampshadeQuery.Contracts.ProductAgg
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public decimal? Price { get; set; }
        public string PriceWithDiscount { get { if (Price == null) return null; else return ((decimal) (Price - (Price * DiscountRate))).ToMoney(); } }
        public decimal DiscountRate { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }

    }
}
