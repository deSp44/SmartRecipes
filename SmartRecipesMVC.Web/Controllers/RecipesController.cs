using System.Linq;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model.Connections;
using SmartRecipesMVC.Web.Services.Models;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserService _userService;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeService recipeService, IUserService userService, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService;
            _userService = userService;
            _logger = logger;
        }

        // RECIPE
        [HttpGet]
        public IActionResult Index()
        {
            var model = _recipeService.GetAllRecipesForList(9, 1, "", false, _userService.GetUserId());
            _logger.LogInformation($"User {_userService.GetUserId()} viewed private recipes.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            pageNumber ??= 1;
            searchString ??= string.Empty;
            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, false, _userService.GetUserId());
            return View(model);
        }

        // VIEW, ADD, EDIT, REMOVE
        public IActionResult ViewRecipe(int recipeId)
        {
            var recipeModel = _recipeService.GetRecipeDetails(recipeId);
            if (recipeModel.OwnerId == null)
            {
                return View(recipeModel);
            }
            else if (recipeModel.OwnerId == _userService.GetUserId())
            {
                _logger.LogInformation($"User {_userService.GetUserId()} view recipe with id: {recipeId}.");
                return View(recipeModel);
            }
            return Forbid();
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View(new NewRecipeVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRecipe(
            [Bind("Id,OwnerId,Name,Description,CreateDate,PreparationTime,Portions,Preparation,Hints,Difficulty,IsActive,RecipeIngredients,Images,Weight,Quantity")]
            NewRecipeVm model)
        {
            if (ModelState.IsValid)
            {
                model.OwnerId = _userService.GetUserId();

                    var file = HttpContext.Request.Form.Files.FirstOrDefault();

                    var id = _recipeService.AddRecipe(model, file);
                _logger.LogInformation($"User {_userService.GetUserId()} added recipe with id: {id}.");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditRecipe(int id)
        {
            var recipe = _recipeService.GetRecipeForEdit(id);
            if (recipe.OwnerId == _userService.GetUserId())
                return View(recipe);

            return Forbid();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRecipe(
            [Bind("Id,OwnerId,Name,Description,CreateDate,PreparationTime,Portions,Preparation,Hints,Difficulty,IsActive,RecipeIngredients,Weight,Quantity")]
            NewRecipeVm model)
        {
            if (ModelState.IsValid)
            {
                _recipeService.UpdateRecipe(model);
                _logger.LogInformation($"User {_userService.GetUserId()} edited recipe with id: {model.Id}.");
                return RedirectToAction("ViewRecipe", new { recipeId = model.Id });
            }
            return View(model);
        }

        public IActionResult MoveToTrash(int id)
        {
            var recipe = _recipeService.GetRecipeDetails(id);
            if (recipe.OwnerId == _userService.GetUserId())
            {
                _recipeService.MoveToTrash(id);
                _logger.LogInformation($"Recipe {id} was moved to trash by user {_userService.GetUserId()}.");
                return RedirectToAction("Index", "Recipes");
            }
            return Forbid();
        }
        
        // DYNAMIC ADD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient([Bind("RecipeIngredients")] NewRecipeVm newRecipeVm)
        {
            newRecipeVm.RecipeIngredients.Add(new RecipeIngredient());
            return PartialView("RecipeIngredients", newRecipeVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveIngredient([Bind("RecipeIngredients")] NewRecipeVm newRecipeVm)
        {
            if (newRecipeVm.RecipeIngredients.Count != 0)
            {
                newRecipeVm.RecipeIngredients.RemoveAt(newRecipeVm.RecipeIngredients.Count - 1);
                return PartialView("RecipeIngredients", newRecipeVm);
            }
            return PartialView("RecipeIngredients", newRecipeVm);
        }

        // PUBLIC
        [HttpGet]
        public IActionResult Public()
        {
            var model = _recipeService.GetAllPublicRecipes(9, 1, "");
            _logger.LogInformation($"User {_userService.GetUserId()} viewed public recipes.");
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Public(int pageSize, int? pageNumber, string searchString)
        {
            pageNumber ??= 1;
            searchString ??= string.Empty;

            var model = _recipeService.GetAllPublicRecipes(pageSize, pageNumber.Value, searchString);
            return View(model);
        }

        // TRASH
        [HttpGet]
        public IActionResult Trash()
        {
            var model = _recipeService.GetAllRecipesForList(9, 1, "", true, _userService.GetUserId());
            _logger.LogInformation($"User {_userService.GetUserId()} viewed trash.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Trash(int pageSize, int? pageNumber, string searchString)
        {
            pageNumber ??= 1;
            searchString ??= string.Empty;

            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, true, _userService.GetUserId());
            return View(model);
        }

        public IActionResult DeleteRecipe(int id)
        {
            var recipe = _recipeService.GetRecipeDetails(id);
            if (recipe.OwnerId == _userService.GetUserId())
            {
                _recipeService.DeleteRecipe(id);
                _logger.LogInformation($"Recipe {id} was permanently deleted!"); 
                return RedirectToAction("Trash", "Recipes");
            }
            return Forbid();
        }

        public IActionResult RestoreRecipe(int id)
        {
            var recipe = _recipeService.GetRecipeDetails(id);
            if (recipe.OwnerId == _userService.GetUserId())
            {
                _recipeService.RestoreRecipe(id);
                _logger.LogInformation($"Recipe {id} was restored!"); 
                return RedirectToAction("Trash", "Recipes");
            }
            return Forbid();
        }

        public IActionResult CreatePdf(int id)
        {
            return RedirectToAction("ViewRecipe", new { recipeId = id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
