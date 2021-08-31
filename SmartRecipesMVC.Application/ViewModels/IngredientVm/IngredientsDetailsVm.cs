using System.ComponentModel;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;

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
            profile.CreateMap<Domain.Model.Ingredient, IngredientsDetailsVm>();
        }
    }
}