using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartRecipesMVC.Application.Interfaces;

namespace SmartRecipesMVC.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var model = _recipeService.GetAllRecipesForList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }
        /*
        [HttpPost]
        public IActionResult AddRecipe(RecipeModel model)
        {
            var id = _recipeService.AddRecipe(model);
            return View();
        }
        */
        public IActionResult ViewRecipe(int recipeId)
        {
            var recipeModel = _recipeService.GetRecipeDetails(recipeId);
            return View(recipeModel);
        }
    }
}
