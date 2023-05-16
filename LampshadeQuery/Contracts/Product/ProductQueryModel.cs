using Framework.Application;

namespace LampshadeQuery.Contracts.Product
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
        public decimal Price { get; set; } = 0;
        public decimal DiscountPercentage { get; set; } = 0;
        public string BasePriceUI { get { return Price.ToMoney(); } }
        public string FinalPriceUI { get { return (Price - Price * (DiscountPercentage / 100)).ToMoney(); } }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }

    }
}
