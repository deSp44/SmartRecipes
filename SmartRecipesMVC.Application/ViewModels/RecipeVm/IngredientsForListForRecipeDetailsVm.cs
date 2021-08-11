using System.ComponentModel;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
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