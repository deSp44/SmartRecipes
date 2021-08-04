namespace SmartRecipesMVC.Domain.Model
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int? Weight { get; set; }
        public int? Quantity { get; set; }
    }
}