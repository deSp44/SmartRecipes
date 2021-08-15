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
            // TODO : DO I NEED INCLUDES?
            return _context.Recipes
                .Include(x => x.RecipeIngredients).ThenInclude(s => s.Ingredient)
                .Include(x => x.Difficulty).ThenInclude(s => s.Recipes)
                .Include(x => x.Images).ThenInclude(s => s.Recipe)
                .FirstOrDefault(x => x.Id == recipeId);
        }
        public int AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return recipe.Id;
        }

        public void DeleteRecipe(int recipeId)
        {
            throw new NotImplementedException();
/*
            var recipe = _context.Recipes.Find(recipeId);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
*/
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
