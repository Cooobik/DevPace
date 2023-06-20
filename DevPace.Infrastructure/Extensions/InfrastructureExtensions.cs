using DevPace.Domain.Interfaces;
using DevPace.Infrastructure.Data;
using DevPace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DevPace.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("serilog.config.json")
                .Build())
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddLogging(builder => builder.ClearProviders().AddSerilog(logger));

            services.AddSwaggerGen();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

    }
}
