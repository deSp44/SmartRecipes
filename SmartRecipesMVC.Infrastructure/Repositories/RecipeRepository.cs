using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartRecipesMVC.Domain.Model.Connections;

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

        public IQueryable<Recipe> GetAllDeletedRecipes()
        {
            return _context.Recipes.Where(x => x.IsActive == false);
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

        public void UpdateRecipe(Recipe recipe)
        {
            var recipeIngredient =
                _context.RecipeIngredient.Where(x => x.RecipeId == recipe.Id).Include(x => x.Ingredient);
            foreach (var ingredient in recipeIngredient)
            {
                if (!recipe.RecipeIngredients.Contains(ingredient))
                {
                    _context.Ingredients.Remove(ingredient.Ingredient);
                    _context.RecipeIngredient.Remove(ingredient);
                }
            }

            _context.Attach(recipe);
            _context.Update(recipe);
            _context.SaveChanges();
        }

        public void MoveToTrash(Recipe recipe)
        {
            _context.Attach(recipe);
            _context.Entry(recipe).Property("IsActive").IsModified = true;
            _context.SaveChanges();
        }

        public IQueryable<Recipe> GetAllPublicRecipes()
        {
            return _context.Recipes.Where(x => x.IsActive && x.OwnerId == null);
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

        public void RestoreRecipe(int recipeId)
        {
            var recipe = _context.Recipes.Find(recipeId);
            if (recipe != null)
            {
                recipe.IsActive = true;
                _context.Attach(recipe);
                _context.Entry(recipe).Property("IsActive").IsModified = true;
                _context.SaveChanges();
            }
        }

    }
}
