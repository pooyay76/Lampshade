﻿using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel entity);
    }
}
