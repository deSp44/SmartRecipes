using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IHostEnvironment _environment;

        public RecipesController(IRecipeService recipeService, IHostEnvironment environment)
        {
            _recipeService = recipeService;
            _environment = environment;
        }

        [HttpGet] public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _recipeService.GetAllRecipesForList(12, 1, "", false, userId);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost] public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            pageNumber ??= 1;
            searchString ??= string.Empty;
        
            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, false, userId);
            return View(model);
        }

        /*[HttpGet]
        public IActionResult Public()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _recipeService.GetAllRecipesForList(12, 1, "", false, userId);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Public(int pageSize, int? pageNumber, string searchString)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            pageNumber ??= 1;
            searchString ??= string.Empty;

            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, false, userId);
            return View(model);
        }*/

        public IActionResult ViewRecipe(int recipeId)
        {
            var recipeModel = _recipeService.GetRecipeDetails(recipeId);
            return View(recipeModel);
        }

        [HttpGet] public IActionResult AddRecipe()
        {
            return View(new NewRecipeVm());
        }

        [ValidateAntiForgeryToken]
        [HttpPost] public IActionResult AddRecipe([Bind("Name,Description,CreateDate,PreparationTime,Portions,Preparation,Hints,DifficultyId,IsActive,RecipeIngredients,Images")]NewRecipeVm model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.OwnerId = userId;

            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var files = HttpContext.Request.Form.Files;
                model.Images = new List<Image>();
                var index = 1;
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim();
                    var fileExt = Path.GetExtension(fileName);
                    var myUniqueFileName = (model.Name + "_" + model.CreateDate.ToString("dd-MM-yyyy") + "-" + Convert.ToString(Guid.NewGuid())).Trim();
                    var newFileName = myUniqueFileName + fileExt;
                    fileName = Path.Combine(_environment.ContentRootPath, @"wwwroot/Content/Images/") + newFileName;

                    using FileStream fs = System.IO.File.Create(fileName);
                    file.CopyTo(fs);
                    fs.Flush();

                    var pathDb = @"~/Content/Images/" + newFileName;
                    var image = new Image
                    {
                        Title = newFileName,
                        ImagePath = pathDb,
                        IsMainImage = index == 1
                    };
                    
                    model.Images.Add(image);

                    index++;
                }
            }

            var id = _recipeService.AddRecipe(model);
            return RedirectToAction("Index");
        }

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
            newRecipeVm.RecipeIngredients.RemoveAt(newRecipeVm.RecipeIngredients.Count - 1);
            return PartialView("RecipeIngredients", newRecipeVm);
        }

        [HttpGet] public IActionResult EditRecipe(int id)
        {
            var customer = _recipeService.GetRecipeForEdit(id);
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost] public IActionResult EditRecipe(NewRecipeVm model)
        {
            _recipeService.UpdateRecipe(model);
            return RedirectToAction("Index");
        }

        public IActionResult MoveToTrash(int id)
        {
            _recipeService.MoveToTrash(id);
            return RedirectToAction("Index");
        }

    }
}
