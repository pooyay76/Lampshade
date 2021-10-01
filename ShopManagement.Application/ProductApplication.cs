using Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public OperationResult Create(CreateProduct dataEntry)
        {
            
            var operation = new OperationResult();
            if (productRepository.Exists(x => x.Name == dataEntry.Name))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            string slug = dataEntry.Name.Slugify();
            var data = new Domain.ProductAgg.ProductViewModel(dataEntry.Name, dataEntry.UnitPrice, dataEntry.Description, dataEntry.IsInStock, dataEntry.Picture,
                dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription,
                slug, dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription) ;
            productRepository.Create(data);
            productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct dataEntry)
        {
            var operation = new OperationResult();
            var entity = productRepository.Get(dataEntry.Id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            if (productRepository.Exists(x => x.Name == dataEntry.Name && x.Id != dataEntry.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            string slug = dataEntry.Name.Slugify();
            entity.Edit(dataEntry.Name, dataEntry.UnitPrice,dataEntry.IsInStock, dataEntry.Description, dataEntry.Picture,
    dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription,
    slug, dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription);
            productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProduct EditGet(long id)
        {
            var entity = productRepository.Get(id);
            if (entity == null)
                return null;
            return new EditProduct()
            {
                CategoryId = entity.CategoryId,
                Code = entity.Code,
                Description = entity.Description,
                Id = entity.Id,
                IsInStock = entity.IsInStock,
                Keywords = entity.Keywords,
                MetaDescription = entity.MetaDescription,
                Name = entity.Name,
                Picture = entity.Picture,
                PictureAlt = entity.PictureAlt,
                PictureTitle = entity.PictureTitle,
                ShortDescription = entity.ShortDescription,
                Slug = entity.Slug,
                UnitPrice = entity.UnitPrice
            };
        }

        public Contracts.ProductAgg.ProductViewModel Get(long id)
        {
           
            var entity = productRepository.Get(id);
            if (entity == null)
                return null;
            return new Contracts.ProductAgg.ProductViewModel()
            {
                CategoryName = entity.Category.Name,
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description,
                Id = entity.Id,
                Keywords = entity.Keywords,
                Picture = entity.Picture,
                PictureAlt = entity.PictureAlt,
                PictureTitle = entity.PictureTitle,
                ShortDescription = entity.ShortDescription,
                UnitPrice = entity.UnitPrice,
            };

        }

        public List<ProductMinimalViewModel> List() 
        {
            return productRepository.ListProductsWithCategories().Select(x => new ProductMinimalViewModel()
            {
                CategoryName = x.Category.Name,
                Name = x.Name,
                Code = x.Code,
                Id = x.Id,
                Picture = x.Picture,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice

            }).ToList();
        }

        public OperationResult MakeInStock(long id)
        {
            var operation = new OperationResult();
            var entity = productRepository.Get(id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            entity.InStock();
            productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult MakeNotInStock(long id)
        {
            var operation = new OperationResult();
            var entity = productRepository.Get(id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            entity.NotInStock();
            productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductMinimalViewModel> Search(ProductSearchModel query)
        {
            if (query == null)
                return List();
            return productRepository.Search(query).Select(x=>new ProductMinimalViewModel() { 
            CategoryName=x.Category.Name,
            Code = x.Code,
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            ShortDescription =x.ShortDescription
            }).ToList();
        }
    }
}
