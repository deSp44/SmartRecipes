using System.ComponentModel;
using System.Data;
using AutoMapper;
using FluentValidation;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;

namespace SmartRecipesMVC.Application.ViewModels.IngredientVm
{
    public class IngredientsDetailsVm : IMapFrom<Domain.Model.Ingredient>
    {
        public int Id { get; set; }
        [DisplayName("Ingredient")] public string Name { get; set; }
        [DisplayName("Weight")] public int? Weight { get; set; }
        [DisplayName("Quantity")] public int? Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IngredientsDetailsVm, Domain.Model.Connections.RecipeIngredient>().ReverseMap();
        }
    }
}