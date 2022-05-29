﻿
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class CreateInventory
    {
        public decimal UnitPrice { get; set; }
        public long ProductId { get; set; }
        public SelectList Products { get; set; }

    }


}
