using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext context;

        public SlideRepository(ShopContext context):base(context)
        {
            this.context = context;
        }

            public List<SlideViewModel> Search(SearchSlide query)
        {
            var items = context.Slides.AsNoTracking().Select(x => new SlideViewModel() 
            {
            Link = x.Link,
            BtnColor = x.BtnColor,
            BtnText = x.BtnText,
            Heading = x.Heading,
            Id = x.Id,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Text = x.Text,
            Title = x.Title
            });

            if (!string.IsNullOrWhiteSpace(query.Text))
                items.Where(x => x.Text.Contains(query.Text));
            if (!string.IsNullOrWhiteSpace(query.Title))
                items.Where(x => x.Text.Contains(query.Title));
            if (!string.IsNullOrWhiteSpace(query.Heading))
                items.Where(x => x.Text.Contains(query.Heading));
            return items.ToList();

        }
    }
}
