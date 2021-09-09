using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Interface;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

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

        public ListRecipeForListVm GetAllRecipesForList(int pageSize, int pageNumber, string searchString, bool trash, string userId)
        {
            List<RecipeForListVm> recipes;
            if(!trash)
            {
                recipes = _recipeRepository.GetAllActiveRecipes()
                    .Where(p => p.Name.StartsWith(searchString) && p.OwnerId == userId)
                    .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider)
                    .ToList();
            }
            else
            {
                recipes = _recipeRepository.GetAllDeletedRecipes()
                    .Where(p => p.Name.StartsWith(searchString))
                    .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider)
                    .ToList();
            }

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

        public int AddRecipe(NewRecipeVm newRecipe)
        {
            var recipe = _mapper.Map<Recipe>(newRecipe);
            var id = _recipeRepository.AddRecipe(recipe);
            return id;
        }

        public NewRecipeVm GetRecipeForEdit(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            var customerVm = _mapper.Map<NewRecipeVm>(recipe);
            return customerVm;
        }

        public void UpdateRecipe(NewRecipeVm model)
        {
            var recipe = _mapper.Map<Recipe>(model);
            _recipeRepository.UpdateRecipe(recipe);
        }

        public void MoveToTrash(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            recipe.IsActive = false;
            _recipeRepository.MoveToTrash(recipe);
        }

        // PUBLIC
        public ListRecipeForListVm GetAllPublicRecipes(int pageSize, int pageNumber, string searchString)
        {
            var recipes = _recipeRepository.GetAllPublicRecipes()
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


        // TRASH
        public void DeleteRecipe(int id)
        {
            _recipeRepository.DeleteRecipe(id);
        }

        public void RestoreRecipe(int id)
        {
            _recipeRepository.RestoreRecipe(id);
        }

    }
}
