using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public string AuthenticateUser()
        {
            string _userId;
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                _userId = userIdClaim.Value;
                return _userId;
            }
            return null;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = AuthenticateUser();
            var model = _recipeService.GetAllRecipesForList(12, 1, "", true, userId);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNumber, string searchString)
        {
            var userId = AuthenticateUser();
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
