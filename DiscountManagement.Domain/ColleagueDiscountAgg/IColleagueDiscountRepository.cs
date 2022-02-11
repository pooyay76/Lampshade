using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        public IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel entity);
        public EditColleagueDiscount EditGet(long id);
    }
}
