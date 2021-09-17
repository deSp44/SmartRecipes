using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.ImageVm;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IHostEnvironment environment, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _environment = environment;
            _mapper = mapper;
        }

        public ListOfImagesVm GetAllImagesForList(int recipeId, string userId)
        {
            var recipeImages = _imageRepository.GetAllRecipeImages(recipeId)
                .ProjectTo<ImageDetailsVm>(_mapper.ConfigurationProvider)
                .ToList();
            var recipeImagesList = new ListOfImagesVm()
            {
                RecipeId = recipeId,
                Images = recipeImages,
                Count = recipeImages.Count
            };
            return recipeImagesList;
        }

        public ListOfImagesVm GetAllImagesForIsMainImage(int recipeId, string userId, int imageRadio)
        {
            var recipeImages = _imageRepository.ChangeIsMainImage(recipeId, imageRadio)
                .ProjectTo<ImageDetailsVm>(_mapper.ConfigurationProvider)
                .ToList();

            var recipeImagesList = new ListOfImagesVm()
            {
                RecipeId = recipeId,
                Images = recipeImages,
                Count = recipeImages.Count
            };
            return recipeImagesList;
        }

        public int AddNewImage(int recipeId, IFormFile file)
        {
            var recipe = _imageRepository.GetRecipe(recipeId);

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                .FileName.ToString()
                .Trim();

            var fileExt = Path.GetExtension(fileName);

            var myUniqueFileName = (recipe.Name + "_" + recipe.CreateDate.ToString("dd-MM-yyyy") + "-" +
                                    Convert.ToString(Guid.NewGuid())).Trim();

            var newFileName = myUniqueFileName + fileExt;
            fileName = Path.Combine(_environment.ContentRootPath, @"wwwroot/Content/Images/") + newFileName;

            using FileStream fs = File.Create(fileName);
            file.CopyTo(fs);
            fs.Flush();

            var pathDb = @"~/Content/Images/" + newFileName;
            var image = new Image
            {
                RecipeId = recipeId,
                Title = myUniqueFileName, 
                Ext = fileExt,
                ImagePath = pathDb,
                IsMainImage = false
            };

            _imageRepository.AddImage(image);

            return recipeId;
        }

        public int DeleteImageFromRecipe(int imageId)
        {
            var recipeId = _imageRepository.DeleteImage(imageId);
            return recipeId;
        }
    }
}
