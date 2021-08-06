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
        ListRecipeForListVm GetAllRecipesForList(int pageSize, int? pageNumber, string searchString);
        RecipeDetailsVm GetRecipeDetails(int recipeid);
        int AddRecipe(NewRecipeVm recipe);
    }
}
