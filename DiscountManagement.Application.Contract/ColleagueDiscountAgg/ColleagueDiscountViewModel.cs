using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscountAgg
{
    public class ColleagueDiscountViewModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal DiscountRate { get; set; }
        public string CreationDate { get; set; }
    }

}