using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Interface;

namespace SmartRecipesMVC.Infrastructure.Repositories
{
    public class TrashRepository : ITrashRepository
    {
        private readonly Context _context;

        public TrashRepository(Context context)
        {
            _context = context;
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
