using System.ComponentModel;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;

namespace SmartRecipesMVC.Application.ViewModels.IngredientVm
{
    public class IngredientsForListVm : IMapFrom<Domain.Model.Ingredient>
    {
        public int Id { get; set; }
        [DisplayName("Składnik")] public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Ingredient, IngredientsForListVm>();
        }
    }
}
