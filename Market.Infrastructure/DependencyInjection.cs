using Market.Application.Repository;
using Market.Application.Services;
using Market.Infrastructure.Repository;
using Market.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddTransient<IAttributeService, AttributeService>();


            services.AddTransient<ILoginService, LoginService>();

            return services;
        }
    }
}

