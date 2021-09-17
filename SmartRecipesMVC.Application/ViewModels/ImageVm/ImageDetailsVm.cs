using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application.ViewModels.ImageVm
{
    public class ImageDetailsVm : IMapFrom<Domain.Model.Image>
    {
        public int Id { get; set; }
        [DisplayName("Name")] public string Title { get; set; }
        [DisplayName("Extension")] public string Ext { get; set; }
        public string ImagePath { get; set; }
        [DisplayName("Main image")] public bool IsMainImage { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Image, ImageDetailsVm>();
        }
    }
}
