using Framework.Domain;
using System;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long ProductId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }
        public decimal DiscountPercentage { get; private set; }

        public CustomerDiscount(long productId, DateTime startDate, DateTime endDate, string reason, decimal discountPercentage)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            DiscountPercentage = discountPercentage;
        }
        public void Edit(long productId, DateTime startDate, DateTime endDate, string reason, decimal discountPercentage)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            DiscountPercentage = discountPercentage;
        }
    }
}
