using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using soclean.dataccess.Contex;
using soclean.dataccess.DataInitalizers;
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
        AddRepositories(services);

        services.AddScoped<DbContextInitalizer>();
        return services;
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPartnerRepository, PartnerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        //services.AddScoped<BaseAuditableInterceptor>();

        services.AddScoped<ISubscribeRepository, SubscribeRepository>();

    }
}