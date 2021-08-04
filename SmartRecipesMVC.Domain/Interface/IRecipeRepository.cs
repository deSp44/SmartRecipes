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
        Recipe GetRecipe(int recipeId);
    }
}
