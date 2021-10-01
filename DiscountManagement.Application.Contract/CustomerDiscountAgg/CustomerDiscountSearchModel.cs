using System;

namespace DiscountManagement.Application.Contract
{
    public class CustomerDiscountSearchModel
    {
        public string Reason { get; set; }
        public long ProductId { get; set; }
        public string StartDateFa { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateFa { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountRate { get; set; }

    }

}
