using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using soclean.dataccess.Contex;
using soclean.dataccess.Repositories.Abstract;
using soclean.dataccess.Repositories.Abstract.Generic;
using soclean.dataccess.Repositories.Implementations;
using soclean.dataccess.Repositories.Implementations.Generic;

namespace soclean.dataccess.ServiceRegistrations;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<DbContextInitalizer>();

        AddRepositories(services);

        return services;
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPartnerRepository, PartnerRepository>();
        services.AddScoped<CategoryRepository, CategoryRepository>();
        //services.AddScoped<BaseAuditableInterceptor>();



    }
}