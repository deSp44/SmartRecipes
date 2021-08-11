using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartRecipesMVC.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly Context _context;

        public RecipeRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Recipe> GetAllActiveRecipes()
        {
            return _context.Recipes.Where(x => x.IsActive);
        }

        public Recipe GetRecipe(int recipeId)
        {
            return _context.Recipes
                .Include(x => x.RecipeIngredients).ThenInclude(s => s.Ingredient)
                .FirstOrDefault(x => x.Id == recipeId);
        }
    }
}
