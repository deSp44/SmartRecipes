using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Application.ViewModels.Recipe;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface IRecipeService
    {
        ListRecipeForListVm GetAllRecipesForList();
        int AddRecipe(NewRecipeVm recipe);
        RecipeDetailsVm GetRecipeDetails(int recipeid);
    }
}
