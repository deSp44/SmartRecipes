using System.Collections.Generic;

namespace SmartRecipesMVC.Domain.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Weight { get; set; }
        public int? Quantity { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}