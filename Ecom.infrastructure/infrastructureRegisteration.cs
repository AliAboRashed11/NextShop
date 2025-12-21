using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositries;
using Ecom.infrastructure.Repositries.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            // Add infrastructure services here

            //Apply Unit of Work 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageManagementService, ImageManagementService>();


            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            //Apply DbContext
            services.AddDbContext<AppDContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CS"));
            });
            return services;
        }

    }
}
