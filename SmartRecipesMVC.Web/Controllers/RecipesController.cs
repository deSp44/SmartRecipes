using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;
using SmartRecipesMVC.Web.Services.Models;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeService recipeService, IHostEnvironment environment, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService;
            _environment = environment;
            _logger = logger;
        }

        // RECIPE
        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _recipeService.GetAllRecipesForList(12, 1, "", false, userId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            pageNumber ??= 1;
            searchString ??= string.Empty;
        
            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, false, userId);
            return View(model);
        }

        // VIEW, ADD, EDIT, REMOVE
        public IActionResult ViewRecipe(int recipeId)
        {
            var recipeModel = _recipeService.GetRecipeDetails(recipeId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"User {userId} view recipe with id: {recipeId}");

            if (recipeModel.OwnerId == userId)
                return View(recipeModel);

            return RedirectToAction("Error");
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.OwnerId = userId;

                // IMAGE UPLOAD
                if (HttpContext.Request.Form.Files.Count != 0)
                {
                    var file = HttpContext.Request.Form.Files.FirstOrDefault();
                    model.Images = new List<Image>();

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                        .FileName.ToString()
                        .Trim();
                    var fileExt = Path.GetExtension(fileName);
                    var myUniqueFileName = (model.Name + "_" + model.CreateDate.ToString("dd-MM-yyyy") + "-" + 
                                            Convert.ToString(Guid.NewGuid())).Trim();
                    var newFileName = myUniqueFileName + fileExt;
                    fileName = Path.Combine(_environment.ContentRootPath, @"wwwroot/Content/Images/") + newFileName;

                    using FileStream fs = System.IO.File.Create(fileName);
                    file.CopyTo(fs);
                    fs.Flush();

                    var pathDb = @"~/Content/Images/" + newFileName;
                    var image = new Image { Title = newFileName, ImagePath = pathDb, IsMainImage = true };
                    model.Images.Add(image);
                }

                var id = _recipeService.AddRecipe(model);
                _logger.LogInformation($"User {userId} added recipe with id: {id}");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditRecipe(int id)
        {
            var recipe = _recipeService.GetRecipeForEdit(id);
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRecipe(
            [Bind("Id,OwnerId,Name,Description,CreateDate,PreparationTime,Portions,Preparation,Hints,Difficulty,IsActive,RecipeIngredients,Images,Weight,Quantity")]
            NewRecipeVm model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _recipeService.UpdateRecipe(model);
                _logger.LogInformation($"User {userId} edited recipe with id: {model.Id}");
                return RedirectToAction("ViewRecipe", new { recipeId = model.Id });
            }
            return View(model);
        }

        public IActionResult MoveToTrash(int id)
        {
            _recipeService.MoveToTrash(id);
            _logger.LogInformation($"Recipe {id} was moved to trash");
            return RedirectToAction("Index");
        }
        
        // DYNAMIC ADD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrderItem([Bind("RecipeIngredients")] NewRecipeVm newRecipeVm)
        {
            newRecipeVm.RecipeIngredients.Add(new RecipeIngredient());
            return PartialView("RecipeIngredients", newRecipeVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveOrderItem([Bind("RecipeIngredients")] NewRecipeVm newRecipeVm)
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
            var model = _recipeService.GetAllPublicRecipes(12, 1, "");
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _recipeService.GetAllRecipesForList(12, 1, "", true, userId);

            ViewData["Action"] = "Trash";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Trash(int pageSize, int? pageNumber, string searchString)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            pageNumber ??= 1;
            searchString ??= string.Empty;

            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, true, userId);
            return View(model);
        }

        public IActionResult DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            _logger.LogInformation($"Recipe {id} was permanently deleted!");
            return RedirectToAction("Trash");
        }

        public IActionResult RestoreRecipe(int id)
        {
            _recipeService.RestoreRecipe(id);
            _logger.LogInformation($"Recipe {id} was restored!");
            return RedirectToAction("Trash");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
