using DevPace.Application.Mappers;
using DevPace.Application.Services;
using DevPace.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevPace.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CustomerMapper));

            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }

    }
}
