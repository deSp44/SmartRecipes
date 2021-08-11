using System.Collections.Generic;
using System.ComponentModel;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class NewRecipeVm
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")] public string Name { get; set; }
        [DisplayName("Opis")] public string Description { get; set; }
        [DisplayName("Trudność przygotowania")] public short DifficultyId { get; set; }
        [DisplayName("Czas przygotowania")] public short PreparationTime { get; set; }
        [DisplayName("Porcje")] public short Portions { get; set; }
        [DisplayName("Przygotowanie")] public string Preparation { get; set; }
        [DisplayName("Wskazówki")] public string Hints { get; set; }
        [DisplayName("Składniki")] public IList<IngredientsForListForRecipeDetailsVm> RecipeIngredients { get; set; }
    }
}
