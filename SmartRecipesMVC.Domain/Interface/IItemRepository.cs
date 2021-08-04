using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Domain.Interface
{
    public interface IItemRepository
    {
        int AddRecipe(Recipe recipe);

        void DeleteRecipe(int recipeId);

        IQueryable<Recipe> GetRecipesByDifficultyId(int difficultyId);

        Recipe GetRecipeById(int recipeId);

        IQueryable<Tag> GetAllTags();

        IQueryable<Difficulty> GetAllDifficuties();
    }
}
