﻿using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public class ProductCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string CreationDateTime { get; set; }
    }

}
