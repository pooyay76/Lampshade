using _0_Framework.Application;
using AccountManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configurations;
using Framework.Infrastructure;
using InventoryManagement.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Application.AutoMapperProfiles;
using ShopManagement.Configuration;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("LampshadeDb");
        }
        private string ConnectionString { get; set; }
        public IConfiguration Configuration { get; }
         
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            FrameworkBootstrapper.Configure(services);
            ShopManagementBootstrapper.Configure(services, ConnectionString);
            DiscountManagementBootstrapper.Configure(services, ConnectionString);
            InventoryManagementBootstrapper.Configure(services, ConnectionString);
            AccountManagementBootstrapper.Configure(services, ConnectionString);
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
