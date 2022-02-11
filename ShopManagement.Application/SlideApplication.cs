using AutoMapper;
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
        private readonly IMapper mapper;

        public SlideApplication(ISlideRepository slideRepository, IMapper mapper)
        {
            this.slideRepository = slideRepository;
            this.mapper = mapper;
        }

        public OperationResult Create(CreateSlide slide)
        {
            var operation = new OperationResult();
            if (slideRepository.Exists(x => x.Title == slide.Title && x.Text == slide.Text && x.Heading == slide.Heading && x.Picture==slide.Picture))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            var entity = new Slide(slide.Picture,slide.Link, slide.PictureTitle, slide.PictureAlt, slide.Heading,
                slide.Title, slide.Text, slide.BtnText, slide.BtnColor);
            slideRepository.Create(entity);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var target = slideRepository.GetSlide(command.Id);
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            if (slideRepository.Exists(x=>x.Picture == command.Picture))
            target.Edit(command.Picture,command.Link, command.PictureTitle, command.PictureAlt, command.Heading,
                command.Title, command.Text, command.BtnText, command.BtnColor);
            slideRepository.Update(target);
            return operation.Succeeded();
        }

        public EditSlide EditGet(long id)
        {
            var target = slideRepository.GetSlide(id);
            if (target == null)
                return null;
            return mapper.Map<Slide, EditSlide>(target);
        }

        public SlideViewModel Get(long id)
        {
            var target = slideRepository.GetSlide(id);
            if (target == null)
                return null;
            return mapper.Map<Slide, SlideViewModel>(target);
        }

        public List<SlideViewModel> List()
        {
            return slideRepository.GetSlides().Select(x => mapper.Map<Slide, SlideViewModel>(x)).ToList();
        }

        public List<SlideViewModel> Search(SearchSlide query)
        {
            return slideRepository.Search(query).Select(x => mapper.Map<Slide, SlideViewModel>(x)).ToList();
        }
    }
}
