using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.Services;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
