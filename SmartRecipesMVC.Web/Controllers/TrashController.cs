using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SmartRecipesMVC.Application.Interfaces;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class TrashController : Controller
    {
        private readonly ITrashService _trashService;
        private readonly IRecipeService _recipeService;

        public TrashController(ITrashService trashService, IRecipeService recipeService)
        {
            _trashService = trashService;
            _recipeService = recipeService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _recipeService.GetAllRecipesForList(12, 1, "", true, userId);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            pageNumber ??= 1;
            searchString ??= string.Empty;

            var model = _recipeService.GetAllRecipesForList(pageSize, pageNumber.Value, searchString, true, userId);
            return View(model);
        }

        public IActionResult DeleteRecipe(int id)
        {
            _trashService.DeleteRecipe(id);
            return RedirectToAction("Index");
        }
        public IActionResult RestoreRecipe(int id)
        {
            _trashService.RestoreRecipe(id);
            return RedirectToAction("Index");
        }
    }
}
