using Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contract
{
    public interface ICustomerDiscountApplication
    {
        public OperationResult Edit(EditCustomerDiscount command);
        public OperationResult Define(DefineCustomerDiscount command);
        public CustomerDiscountViewModel EditGet(long id);
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel query);
    }

}
