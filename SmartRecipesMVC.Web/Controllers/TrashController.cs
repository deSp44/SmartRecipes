using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Web.Helpers;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class TrashController : Controller
    {
        private readonly ITrashService _trashService;
        private readonly IRecipeService _recipeService;
        private readonly AuthenticateUser _authenticateUser;

        public TrashController(ITrashService trashService, IRecipeService recipeService, AuthenticateUser authenticateUser)
        {
            _trashService = trashService;
            _recipeService = recipeService;
            _authenticateUser = authenticateUser;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var userId = _authenticateUser.GetUserId();
            var model = _recipeService.GetAllRecipesForList(12, 1, "", true, userId);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            var userId = _authenticateUser.GetUserId();
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
