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
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == recipeId);
        }
        public int AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return recipe.Id;
        }

        public void UpdateCustomer(Recipe recipe)
        {
            _context.Attach(recipe);
            _context.Entry(recipe).Property("Name").IsModified = true;
            _context.Entry(recipe).Property("Description").IsModified = true;
            _context.Entry(recipe).Property("PreparationTime").IsModified = true;
            _context.Entry(recipe).Property("Portions").IsModified = true;
            _context.Entry(recipe).Property("Preparation").IsModified = true;
            _context.Entry(recipe).Property("Hints").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteRecipe(int recipeId)
        {
            var recipe = _context.Recipes.Find(recipeId);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
        }

        public IQueryable<Recipe> GetRecipesByDifficultyId(int difficultyId)
        {
            throw new NotImplementedException();
/*
            var recipes = _context.Recipes.Where(x => x.DifficultyId == difficultyId);
            return recipes;
*/
        }
    }
}
