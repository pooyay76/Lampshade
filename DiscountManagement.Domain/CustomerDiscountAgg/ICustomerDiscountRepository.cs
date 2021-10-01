using DiscountManagement.Application.Contract;
using Framework.Domain;
using System.Collections.Generic;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        public EditCustomerDiscount EditGet(long id);
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command);
    }
}
