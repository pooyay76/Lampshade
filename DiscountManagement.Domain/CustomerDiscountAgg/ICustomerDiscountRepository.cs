using DiscountManagement.Application.Contracts;
using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        public IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel entity);
    }
}
