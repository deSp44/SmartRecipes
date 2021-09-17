using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Application.ViewModels.ImageVm;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface IImageService
    {
        ListOfImagesVm GetAllImagesForList(int recipeId, string userId);
        ListOfImagesVm GetAllImagesForIsMainImage(int recipeId, string userId, int imageRadio);
        int DeleteImageFromRecipe(int imageId);
    }
}
