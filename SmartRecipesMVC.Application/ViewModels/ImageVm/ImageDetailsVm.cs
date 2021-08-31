using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;

namespace SmartRecipesMVC.Application.ViewModels.ImageVm
{
    public class ImageDetailsVm : IMapFrom<Domain.Model.Image>
    {
        public int Id { get; set; }
        [DisplayName("Title")] public string Title { get; set; }
        public string ImagePath { get; set; }
        public bool IsMainImage { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Image, ImageDetailsVm>();
        }
    }
}
