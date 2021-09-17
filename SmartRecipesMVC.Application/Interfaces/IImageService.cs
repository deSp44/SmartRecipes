using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartRecipesMVC.Application.ViewModels.ImageVm;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface IImageService
    {
        ListOfImagesVm GetAllImagesForList(int recipeId, string userId);
        ListOfImagesVm GetAllImagesForIsMainImage(int recipeId, string userId, int imageRadio);
        int AddNewImage(int recipeId, IFormFile file);
        int DeleteImageFromRecipe(int imageId);
    }
}
