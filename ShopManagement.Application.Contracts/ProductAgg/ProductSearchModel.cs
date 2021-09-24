namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductSearchModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsInStock { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
    }
}
