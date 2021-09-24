using Framework.Application;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            this.slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide slide)
        {
            var operation = new OperationResult();
            if (slideRepository.Exists(x => x.Title == slide.Title && x.Text == slide.Text && x.Heading == slide.Heading))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            var entity = new Slide(slide.Picture,slide.Link, slide.PictureTitle, slide.PictureAlt, slide.Heading,
                slide.Title, slide.Text, slide.BtnText, slide.BtnColor);
            slideRepository.Create(entity);
            slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide slide)
        {
            var operation = new OperationResult();
            var target = slideRepository.Get(slide.Id);
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            target.Edit(slide.Picture,slide.Link, slide.PictureTitle, slide.PictureAlt, slide.Heading,
                slide.Title, slide.Text, slide.BtnText, slide.BtnColor);
            slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlide EditGet(long id)
        {
            var target = slideRepository.Get(id);
            if (target == null)
                return null;
            return new EditSlide()
            {
                Link = target.Link,
                BtnColor = target.BtnColor,
                BtnText = target.BtnText,
                Heading = target.Heading,
                Id = target.Id,
                Picture = target.Picture,
                PictureAlt = target.PictureAlt,
                PictureTitle = target.PictureTitle,
                Text = target.Text,
                Title = target.Title
            };
        }

        public SlideViewModel Get(long id)
        {
            var target = slideRepository.Get(id);
            if (target == null)
                return null;
            return new SlideViewModel()
            {
                Link = target.Link,
                BtnColor = target.BtnColor,
                BtnText = target.BtnText,
                Heading = target.Heading,
                Id = target.Id,
                Picture = target.Picture,
                PictureAlt = target.PictureAlt,
                PictureTitle = target.PictureTitle,
                Text = target.Text,
                Title = target.Title
            };
        }

        public List<SlideViewModel> List()
        {
            return slideRepository.List().Select(x => new SlideViewModel()
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
            }).ToList();
        }

        public List<SlideViewModel> Search(SearchSlide query)
        {
            return slideRepository.Search(query);
        }
    }
}
