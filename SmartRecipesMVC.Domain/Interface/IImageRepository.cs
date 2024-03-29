﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Domain.Interface
{
    public interface IImageRepository
    {
        string GetRecipeOwnerId(int recipeId);
        IQueryable<Image> GetAllRecipeImages(int recipeId);
        IQueryable<Image> ChangeIsMainImage(int recipeId, int imageRadio);
        Recipe GetRecipe(int recipeId);
        void AddImage(Image image);
        int DeleteImage(int imageId);
    }
}
