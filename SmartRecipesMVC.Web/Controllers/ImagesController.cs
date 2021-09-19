using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Application.ViewModels.ImageVm;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Web.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IImageService imageService, IUserService userService, ILogger<ImagesController> logger)
        {
            _imageService = imageService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int recipeId)
        {
            if (_imageService.GetRecipeOwnerId(recipeId) != _userService.GetUserId()) 
                return Forbid();

            var images = _imageService.GetAllImagesForList(recipeId, _userService.GetUserId());
            _logger.LogInformation($"User {_userService.GetUserId()} viewed images for recipe with id {recipeId}.");
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int recipeId, int imageRadio)
        {
            var images = _imageService.GetAllImagesForIsMainImage(recipeId, _userService.GetUserId(), imageRadio);
            return View(images);
        }

        public IActionResult Delete(int imageId, int recipeIdToDelete)
        {
            if (_imageService.GetRecipeOwnerId(recipeIdToDelete) != _userService.GetUserId())
                return Forbid();

            var recipeId = _imageService.DeleteImageFromRecipe(imageId);
            _logger.LogInformation($"User {_userService.GetUserId()} deleted image with id: {imageId}.");
            return RedirectToAction("Index", new { recipeId });
        }

        public IActionResult AddImage(int recipeId)
        {
            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                _imageService.AddNewImage(recipeId, file);
                _logger.LogInformation($"User {_userService.GetUserId()} added image with id to recipe with id: {recipeId}.");
                return RedirectToAction("Index", new { recipeId });
            }

            return RedirectToAction("Index", new { recipeId });
        }
    }
}