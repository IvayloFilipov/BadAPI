using BadApi.Repositories;
using BadApi.Services;
using BadAPI.Data.Interfaces;
using BadAPI.Data.Repositories;
using BadAPI.Services;
using BadAPI.Services.Interfaces;

namespace BadAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IReviewService, ReviewService>();

            return services;
        }
    }
}
