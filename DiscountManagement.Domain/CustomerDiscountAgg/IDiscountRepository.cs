using Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface IDiscountRepository : IRepository<long,CustomerDiscount>
    {

    }
}
