using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface IRecipeService
    {
        ListRecipeForListVm GetAllRecipesForList(int pageSize, int pageNumber, string searchString, bool trash, string userId);
        RecipeDetailsVm GetRecipeDetails(int recipeId);
        int AddRecipe(NewRecipeVm recipe, IFormFile file);
        NewRecipeVm GetRecipeForEdit(int id);
        void UpdateRecipe(NewRecipeVm model);
        void MoveToTrash (int id);

        ListRecipeForListVm GetAllPublicRecipes(int pageSize, int pageNumber, string searchString);

        void DeleteRecipe(int id);
        void RestoreRecipe(int id);
    }
}
