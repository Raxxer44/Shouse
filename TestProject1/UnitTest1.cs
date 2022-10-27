using AutoMapper;
using Technic.Web.Data;
using Technic.Web.Infrastructure.Mapping;
using Technic.Web.Services;
using Technic.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Technic.Web.Data.Contexts;
using Technic.Web.Data.Base;
using Microsoft.AspNetCore.Identity;
using Technic.Web.Data.Entites;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void GetCategoryByIdTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechnicDb;Trusted_Connection=True;");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICategoryService, CategoryService>();
            var serviceProvider = services.BuildServiceProvider();

            var categoryservice = serviceProvider.GetRequiredService<ICategoryService>();
            var result = categoryservice.GetCategoryById(Guid.Parse("7dfed473-5b4a-43b1-8d76-0c55d2f1a769"));
           
            Assert.True(result.Result.Succeeded);
            
        }
        [Fact]
        public void GetAllCategoriesTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechnicDb;Trusted_Connection=True;");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICategoryService, CategoryService>();
            var serviceProvider = services.BuildServiceProvider();

            var categoryservice = serviceProvider.GetRequiredService<ICategoryService>();
            var result = categoryservice.GetAllCategory();

            Assert.True(result.Result.Succeeded);

        }

        [Fact]
        public void GetAllProductsTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechnicDb;Trusted_Connection=True;");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IProductService, ProductService>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            var serviceProvider = services.BuildServiceProvider();

            var productservice = serviceProvider.GetRequiredService<IProductService>();
            var result = productservice.GetProductListByCategory(Guid.NewGuid());

            Assert.True(!result.Result.Succeeded);

        }
    }
}