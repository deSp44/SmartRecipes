using System.Collections.Generic;
using System.ComponentModel;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Domain.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}