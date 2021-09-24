using System.Collections.Generic;

namespace LampshadeQuery.Contracts.SlideAgg
{
    public interface ISlideQuery
    {
        public List<SlideQueryModel> GetSlides();
    }
}
