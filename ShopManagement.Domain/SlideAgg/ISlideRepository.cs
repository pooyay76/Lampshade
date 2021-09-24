using Framework.Domain;
using ShopManagement.Application.Contracts.SlideAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        public List<SlideViewModel> Search(SearchSlide slide);
    }
}
