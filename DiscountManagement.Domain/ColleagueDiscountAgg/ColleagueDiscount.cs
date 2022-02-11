using Framework.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : EntityBase
    {
        public string Name { get; private set; }
        public long ProductId { get; private set; }
        public decimal DiscountRate { get; private set; }
        public bool IsRemoved { get; private set; }

        public ColleagueDiscount(string name,long productId, decimal discountRate)
        {
            Name = name;
            ProductId = productId;
            DiscountRate = discountRate;
        }
        public void Edit(string name, long productId, decimal discountRate)
        {
            Name = name;
            ProductId = productId;
            DiscountRate = discountRate;
        }
        public void Remove()
        {
            IsRemoved = true;
        }
        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
