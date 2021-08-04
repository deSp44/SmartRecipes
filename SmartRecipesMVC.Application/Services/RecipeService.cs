using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.Recipe;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;

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

        public int AddRecipe(NewRecipeVm recipe)
        {
            throw new NotImplementedException();
        }

        public ListRecipeForListVm GetAllRecipesForList()
        {
            var recipes = _recipeRepository.GetAllActiveRecipes()
                .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider).ToList();
            var recipeList = new ListRecipeForListVm()
            {
                Recipes = recipes,
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
    }
}