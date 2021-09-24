namespace DiscountManagement.Application.Contract
{
    public class CustomerDiscountSearchModel
    {
        public string Reason { get; set; }
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountRate { get; set; }

    }

}
