using Framework.Domain;
using System;

namespace CustomerDiscountModel
{
    public class CustomerDiscountModel : EntityBase
    {
        public long ProductId { get;private set; }
        public string Reason { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal DiscountRate { get; private set; }

        public CustomerDiscountModel(long productId, string reason, DateTime startDate, DateTime endDate, decimal discountRate)
        {
            ProductId = productId;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            DiscountRate = discountRate;
        }
        public void Edit (long productId, string reason, DateTime startDate, DateTime endDate, decimal discountRate)
        {
            ProductId = productId;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            DiscountRate = discountRate;
        }
    }
}
