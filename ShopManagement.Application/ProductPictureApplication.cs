using Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore.Repository;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            this.productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture data)
        {
            var operation = new OperationResult();
            if (productPictureRepository.Exists(x =>
             x.Picture == data.Picture &&
             x.ProductId != data.ProductId
            ))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            productPictureRepository.Create(new ProductPicture(data.Picture, data.PictureAlt,
                data.PictureTitle, data.ProductId));
            productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture data)
        {
            var operation = new OperationResult();
            var target = productPictureRepository.Get(data.Id);
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            if (productPictureRepository.Exists(x => x.Picture == data.Picture && x.Id != data.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            target.Edit(data.Picture, data.PictureAlt, data.PictureTitle, data.ProductId);
            return operation.Succeeded();
        }

        public EditProductPicture EditGet(long id)
        {
            var target = productPictureRepository.Get(id);
            if (target == null)
                return null;
            return new EditProductPicture()
            {
                Id = target.Id,
                Picture = target.Picture,
                PictureAlt = target.PictureAlt,
                PictureTitle = target.PictureTitle,
                ProductId = target.ProductId
            };
        }

        public ProductPictureViewModel Get(long id)
        {
            var target = productPictureRepository.Get(id);
            if (target==null)
                return null;
            return new ProductPictureViewModel()
            {
                Id = target.Id,
                Picture = target.Picture,
                PictureAlt = target.PictureAlt,
                PictureTitle = target.PictureTitle,
                ProductName = target.PictureTitle
            };
        }

        public List<ProductPictureMinimalViewModel> List()
        {
            return productPictureRepository.ListProductPicturesWithProducts();
        }

        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture searchModel)
        {
            return productPictureRepository.Search(searchModel);
        }
    }
}
