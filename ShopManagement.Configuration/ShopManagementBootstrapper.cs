using LampshadeQuery.Contracts.ProductAgg;
using LampshadeQuery.Contracts.ProductCategoryAgg;
using LampshadeQuery.Contracts.SlideAgg;
using LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection,string connectionString)
        {

            serviceCollection.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            serviceCollection.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            serviceCollection.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            serviceCollection.AddTransient<IProductApplication, ProductApplication>();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            serviceCollection.AddTransient<IProductQuery, ProductQuery>();

            serviceCollection.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            serviceCollection.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            serviceCollection.AddTransient<ISlideApplication, SlideApplication>();
            serviceCollection.AddTransient<ISlideRepository, SlideRepository>();
            serviceCollection.AddTransient<ISlideQuery, SlideQuery>();

            serviceCollection.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
