﻿namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Keywords { get; set; }
        public string CategoryName { get; set; }
        public string CreationDateTime { get; set; }
    }
}
