using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //Apply DbContext
            services.AddDbContext<AppDContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CS"));
            });
            return services;
        }

    }
}
