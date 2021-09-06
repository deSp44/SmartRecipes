using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.ImageVm;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class RecipeDetailsVm : IMapFrom<Domain.Model.Recipe>
    {
        public int Id { get; set; }
        [DisplayName("Name")] public string Name { get; set; }
        [DisplayName("Description")] public string Description { get; set; }
        [DisplayName("Created date")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        [DisplayName("Preparation time")] public short PreparationTime { get; set; }
        [DisplayName("Portions")] public short Portions { get; set; }
        [DisplayName("Preparation")] public string Preparation { get; set; }
        [DisplayName("Hints")] public string Hints { get; set; }
        [DisplayName("Difficulty")] public string Difficulty { get; set; }

        
        [DisplayName("Ingredients")] public IList<IngredientsDetailsVm> RecipeIngredients { get; set; }
        [DisplayName("Images")] public IList<ImageDetailsVm> Images { get; set; }
        //[DisplayName("Tagi")] public IList<TagsForListForRecipeDetailsVm> RecipeTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Connections.RecipeIngredient, IngredientsDetailsVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Ingredient.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Ingredient.Name))
                .ForMember(d => d.Weight, opt => opt.MapFrom(s => s.Weight))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));

            profile.CreateMap<Domain.Model.Recipe, RecipeDetailsVm>();
        }
    }
}
