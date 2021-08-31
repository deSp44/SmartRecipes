namespace SmartRecipesMVC.Domain.Interface
{
    public interface ITrashRepository
    {
        void DeleteRecipe(int recipeId);
        void RestoreRecipe(int recipeId);
    }
}