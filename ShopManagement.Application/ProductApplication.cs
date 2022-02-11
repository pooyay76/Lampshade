using AutoMapper;
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
        private readonly IMapper mapper;

        public ProductApplication(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public OperationResult Create(CreateProduct dataEntry)
        {
            
            var operation = new OperationResult();
            if (productRepository.Exists(x => x.Name == dataEntry.Name))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            string slug = dataEntry.Name.Slugify();
            var data = new Product(dataEntry.Name, dataEntry.UnitPrice, dataEntry.Description, dataEntry.IsInStock, dataEntry.Picture,
                dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription,
                slug, dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription) ;
            productRepository.Create(data);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct dataEntry)
        {
            var operation = new OperationResult();

            //fetch entity
            var entity = productRepository.GetProduct(dataEntry.Id);

            //if entity not found
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            //if entering duplicated data
            if (productRepository.Exists(x => x.Name == dataEntry.Name && x.Id != dataEntry.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            string slug = dataEntry.Name.Slugify();

            entity.Edit(dataEntry.Name, dataEntry.UnitPrice, dataEntry.IsInStock, dataEntry.Description, dataEntry.Picture,
                dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription, slug,
                dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription);

            productRepository.Update(entity);

            return operation.Succeeded();
        }

        public EditProduct EditGet(long id)
        {
            var entity = productRepository.GetProduct(id);

            //if entity not found return null
            if (entity == null)
                return null;
            return mapper.Map<Product,EditProduct>(entity);
        }

        public ProductViewModel Get(long id)
        {
           
            var entity = productRepository.GetProduct(id);
            if (entity == null)
                return null;
            return mapper.Map<Product, ProductViewModel>(entity);
        }

        public List<ProductMinimalViewModel> List() 
        {
            return productRepository.GetProductsWithCategories().Select(x => mapper.Map<Product,ProductMinimalViewModel>(x)).ToList();
        }

        public OperationResult MakeInStock(long id)
        {
            var operation = new OperationResult();
            var entity = productRepository.GetProduct(id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            entity.InStock();
            productRepository.Update(entity);
            return operation.Succeeded();
        }

        public OperationResult MakeNotInStock(long id)
        {
            var operation = new OperationResult();
            var entity = productRepository.GetProduct(id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            entity.NotInStock();
            productRepository.Update(entity);
            return operation.Succeeded();
        }

        public List<ProductMinimalViewModel> Search(ProductSearchModel query)
        {
            return productRepository.Search(query).Select(x => mapper.Map<Product, ProductMinimalViewModel>(x)).ToList();
        }
    }
}
