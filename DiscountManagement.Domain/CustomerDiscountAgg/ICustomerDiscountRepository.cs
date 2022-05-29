﻿using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        public IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel entity);
    }
}
