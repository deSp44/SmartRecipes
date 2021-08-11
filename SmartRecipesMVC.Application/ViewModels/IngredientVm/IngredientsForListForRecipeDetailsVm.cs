using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Application.ViewModels.IngredientVm
{
    public class IngredientsForListForRecipeDetailsVm : IMapFrom<Domain.Model.Ingredient>
    {
        public int Id { get; set; }
        [DisplayName("Składnik")] public string Name { get; set; }
        [DisplayName("Waga")] public int? Weight { get; set; }
        [DisplayName("Ilość")] public int? Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Ingredient, IngredientsForListForRecipeDetailsVm>();
        }
    }
}