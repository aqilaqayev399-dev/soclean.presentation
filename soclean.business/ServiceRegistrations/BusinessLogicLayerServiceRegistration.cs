using Microsoft.Extensions.DependencyInjection;
using soclean.business.Services.Abstract;
using soclean.business.Services.Abstract.Generic;
using soclean.business.Services.Implementations;
using soclean.business.Services.Implementations.Generic;
using System.Reflection;

namespace soclean.business.ServiceRegistrations
{
    public static class BusinessLogicLayerServiceRegistration
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICloudinaryManager, CloudinaryManager>();
            //services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(ICrudService<,,,>), typeof(CrudService<,,,>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IBlogService, BlogService>();
           
            return services;
        }
    }
}
