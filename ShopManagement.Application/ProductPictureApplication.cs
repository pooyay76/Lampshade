using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository productPictureRepository;
        private readonly IMapper mapper;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IMapper mapper)
        {
            this.productPictureRepository = productPictureRepository;
            this.mapper = mapper;
        }

        public OperationResult Create(CreateProductPicture data)
        {
            var operation = new OperationResult();
            if (productPictureRepository.Exists(x => x.Picture == data.Picture && x.ProductId != data.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            productPictureRepository.Create(new ProductPicture(data.Picture, data.PictureAlt,
                data.PictureTitle, data.ProductId));
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture data)
        {
            var operation = new OperationResult();
            var target = productPictureRepository.GetProductPicture(data.Id);

            //Not Found
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);

            //Duplicated
            if (productPictureRepository.Exists(x => x.Picture == data.Picture && x.Id != data.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            target.Edit(data.Picture, data.PictureAlt, data.PictureTitle, data.ProductId);

            productPictureRepository.Update(target);
            return operation.Succeeded();
        }

        public EditProductPicture EditGet(long id)
        {
            var target = productPictureRepository.GetProductPicture(id);
            if (target == null)
                return null;
            return mapper.Map<ProductPicture, EditProductPicture>(target);
        }

        public ProductPictureViewModel Get(long id)
        {
            var target = productPictureRepository.GetProductPicture(id);
            if (target==null)
                return null;
            return mapper.Map<ProductPicture, ProductPictureViewModel>(target);

        }

        public List<ProductPictureMinimalViewModel> List()
        {
            return productPictureRepository.GetProductPictures().Select(x=>mapper.Map<ProductPicture, ProductPictureMinimalViewModel>(x)).ToList();
        }

        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture searchModel)
        {
            return productPictureRepository.Search(searchModel).Select(x => mapper.Map<ProductPicture, ProductPictureMinimalViewModel>(x)).ToList();
        }
    }
}
