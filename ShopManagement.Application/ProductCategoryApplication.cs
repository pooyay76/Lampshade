using Framework.Application;
using ShopManagement.Application.Contracts;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory form)
        {
            var operationResult = new OperationResult();
            if (productCategoryRepository.Exists(x => form.Name == x.Name))
            {
                return operationResult.Failed("Record already exists");
            }
            string slug = form.Name.Slugify();
            var productCategory = new ProductCategory(form.Name, form.Description, form.Picture, form.PictureAlt, form.PictureTitle,
                form.Keywords, form.MetaDescription, slug);
            productCategoryRepository.Create(productCategory);
            productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory form)
        {
            var operationResult = new OperationResult();
            ProductCategory productCategory = productCategoryRepository.Get(form.Id);
            if (productCategory == null)
                return operationResult.Failed("Could not find the record");
            if (productCategoryRepository.Exists(x => x.Name == form.Name && x.Id == form.Id))
                return operationResult.Failed("Record has the same value that you are trying to submit");
            string slug = form.Name.Slugify();
            productCategory.Edit(form.Name, form.Description, form.Picture, form.PictureAlt,
                form.PictureTitle, form.Keywords, form.MetaDescription, slug);
            productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductCategory EditGet(long id)
        {
            var productCategory = productCategoryRepository.Get(id);
            if (productCategory == null)
                return null;
            return new EditProductCategory()
            {
                Description = productCategory.Description,
                Id = productCategory.Id,
                Keywords = productCategory.Keywords,
                MetaDescription = productCategory.MetaDescription,
                Name = productCategory.Name,
                Picture = productCategory.Picture,
                PictureAlt = productCategory.PictureAlt,
                PictureTitle = productCategory.PictureTitle,
                Slug = productCategory.Slug
            };
        }

        public ProductCategoryViewModel Get(long id)
        {
            var count = productCategoryRepository.Count();
            var productCategory = productCategoryRepository.Get(id);
            if (productCategory == null)
                return null;
            return new ProductCategoryViewModel()
            {
                CreationDate = productCategory.CreationDateTime.ToString(),
                Name = productCategory.Name,
                Picture = productCategory.Picture,
                Id = productCategory.Id,
                ProductsCount = count
            };
        }

        public IEnumerable<ProductCategoryMinimalViewModel> List()
        {
            var result = new List<ProductCategoryMinimalViewModel>();
            if (productCategoryRepository.Count() == 0)
                return null;
            productCategoryRepository.List().ToList().ForEach(x=> result.Add( new ProductCategoryMinimalViewModel() 
            {
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Picture=x.Picture
            }));
            return result.OrderBy(x=>x.Id);
        }

        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchModel)
        {
            return productCategoryRepository.Search(searchModel);
        }

        List<ProductCategoryMinimalViewModel> IProductCategoryApplication.List()
        {
            throw new System.NotImplementedException();
        }
    }
}
