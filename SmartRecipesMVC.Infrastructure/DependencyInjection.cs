using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Infrastructure.Repositories;

namespace SmartRecipesMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRecipeRepository, RecipeRepository>();

            return services;
        }
    }
}
