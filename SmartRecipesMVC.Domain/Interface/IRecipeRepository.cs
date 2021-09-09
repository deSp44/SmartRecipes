using SmartRecipesMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecipesMVC.Domain.Interface
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllActiveRecipes();
        IQueryable<Recipe> GetAllDeletedRecipes();
        Recipe GetRecipe(int recipeId);
        int AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void MoveToTrash(Recipe recipe);

        IQueryable<Recipe> GetAllPublicRecipes();

        void DeleteRecipe(int recipeId);
        void RestoreRecipe(int recipeId);

    }
}
