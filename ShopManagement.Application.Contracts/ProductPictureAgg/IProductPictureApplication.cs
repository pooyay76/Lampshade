using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public interface IProductPictureApplication
    {
        public List<ProductPictureMinimalViewModel> List();
        public ProductPictureViewModel Get(long id);
        public OperationResult Edit(EditProductPicture editProductPicture);
        public OperationResult Create(CreateProductPicture createProductPicture);
        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture searchModel);
        public EditProductPicture EditGet(long id);
    }
}
