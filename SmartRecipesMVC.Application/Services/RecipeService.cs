using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Interface;

namespace SmartRecipesMVC.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public ListRecipeForListVm GetAllRecipesForList(int pageSize, int pageNumber, string searchString)
        {
            var recipes = _recipeRepository.GetAllActiveRecipes()
                .Where(p => p.Name.StartsWith(searchString))
                .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var recipesToShow = recipes.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var recipeList = new ListRecipeForListVm()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                SearchString = searchString,
                Recipes = recipesToShow,
                Count = recipes.Count
            };
            return recipeList;
        }

        public RecipeDetailsVm GetRecipeDetails(int recipeId)
        {
            var recipe = _recipeRepository.GetRecipe(recipeId);
            var recipeVm = _mapper.Map<RecipeDetailsVm>(recipe);
            return recipeVm;
        }

        public int AddRecipe(NewRecipeVm recipe)
        {
            throw new NotImplementedException();
        }
    }
}
