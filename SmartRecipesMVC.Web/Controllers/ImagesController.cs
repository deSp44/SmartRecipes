using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Security.Claims;
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
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IImageService imageService, IHostEnvironment environment, ILogger<ImagesController> logger)
        {
            _imageService = imageService;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var images = _imageService.GetAllImagesForList(recipeId, userId);
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int recipeId, int imageRadio)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var images = _imageService.GetAllImagesForIsMainImage(recipeId, userId, imageRadio);
            return View(images);
        }

        public IActionResult Delete(int imageId)
        {
            var recipeId = _imageService.DeleteImageFromRecipe(imageId);
            return RedirectToAction("Index", new { recipeId });
        }
        public IActionResult AddImage(int recipeId)
        {
            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                _imageService.AddNewImage(recipeId, file);
                return RedirectToAction("Index", new { recipeId });
            }

            return RedirectToAction("Index", new { recipeId });
        }
    }
}
