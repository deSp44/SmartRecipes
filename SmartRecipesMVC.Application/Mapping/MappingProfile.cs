﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SmartRecipesMVC.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                    .Where(x => x.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}
