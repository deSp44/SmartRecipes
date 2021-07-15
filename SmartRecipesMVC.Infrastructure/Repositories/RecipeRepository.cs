using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecipesMVC.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly Context _context;
        public RecipeRepository(Context context)
        {
            _context = context;
        }

        public int AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return recipe.Id;
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
            var recipes = _context.Recipes.Where(x => x.DifficultyId == difficultyId);
            return recipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == recipeId);
            return recipe;
        }



        public IQueryable<Tag> GetAllTags()
        {
            var tags = _context.Tags;
            return tags;
        }

        public IQueryable<Difficulty> GetAllDifficuties()
        {
            var difficulties = _context.Difficulties;
            return difficulties;
        }

    }
}
