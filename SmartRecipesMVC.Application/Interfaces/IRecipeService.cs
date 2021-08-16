using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface IRecipeService
    {
        ListRecipeForListVm GetAllRecipesForList(int pageSize, int pageNumber, string searchString);
        RecipeDetailsVm GetRecipeDetails(int recipeId);
        int AddRecipe(NewRecipeVm recipe);
        NewRecipeVm GetRecipeForEdit(int id);
        void UpdateRecipe(NewRecipeVm model);
        void DeleteRecipe(int id);
    }
}
