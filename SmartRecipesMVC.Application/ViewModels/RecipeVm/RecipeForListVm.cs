using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class RecipeForListVm : IMapFrom<Domain.Model.Recipe>
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")] public string Name { get; set; }
        [DisplayName("Opis")] public string Description { get; set; }
        [DisplayName("Data utworzenia")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public IList<Image> Images { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Recipe, RecipeForListVm>();
        }
    }
}
