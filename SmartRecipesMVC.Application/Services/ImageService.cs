using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.ImageVm;
using SmartRecipesMVC.Domain.Interface;

namespace SmartRecipesMVC.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
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
        public int DeleteImageFromRecipe(int imageId)
        {
            var recipeId = _imageRepository.DeleteImage(imageId);
            return recipeId;
        }
    }
}
