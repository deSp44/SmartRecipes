using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly Context _context;

        public ImageRepository(Context context)
        {
            _context = context;
        }

        public string GetRecipeOwnerId(int recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == recipeId);
            if (recipe != null)
                return recipe.OwnerId;

            return string.Empty;
        }

        public IQueryable<Image> GetAllRecipeImages(int recipeId)
        {
            return _context.Images.Where(x => x.RecipeId == recipeId);
        }

        public IQueryable<Image> ChangeIsMainImage(int recipeId, int imageRadio)
        {
            var imageListForRecipe = _context.Images.Where(x => x.RecipeId == recipeId);
            var index = 0;
            foreach (var image in imageListForRecipe)
            {
                image.IsMainImage = index == imageRadio;
                index++;
                _context.Images.Update(image);
            }
            _context.SaveChanges();
            return _context.Images.Where(x => x.RecipeId == recipeId);
        }

        public Recipe GetRecipe(int recipeId)
        {
            return _context.Recipes.FirstOrDefault(x => x.Id == recipeId);
        }

        public void AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public int DeleteImage(int imageId)
        {
            var imageToDelete = _context.Images.Find(imageId);
            var recipeId = imageToDelete.RecipeId;

            if (imageToDelete != null)
            {
                _context.Images.Remove(imageToDelete);
                _context.SaveChanges();
            }
            return recipeId;
        }
    }
}
