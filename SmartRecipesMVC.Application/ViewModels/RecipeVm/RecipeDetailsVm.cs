using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class RecipeDetailsVm : IMapFrom<Domain.Model.Recipe>
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")] public string Name { get; set; }
        [DisplayName("Data utworzenia")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        [DisplayName("Opis")] public string Description { get; set; }
        [DisplayName("Trudność przygotowania")] public short DifficultyId { get; set; }
        [DisplayName("Czas przygotowania")] public short PreparationTime { get; set; }
        [DisplayName("Porcje")] public short Portions { get; set; }
        [DisplayName("Przygotowanie")] public string Preparation { get; set; }
        [DisplayName("Wskazówki")] public string Hints { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        [DisplayName("Składniki")] public IList<IngredientsForListForRecipeDetailsVm> RecipeIngredients { get; set; }

        //[DisplayName("Tagi")] public ICollection<TagsForListVm> RecipeTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Connections.RecipeIngredient, IngredientsForListForRecipeDetailsVm>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Ingredient.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Ingredient.Name))
                .ForMember(d => d.Weight, o => o.MapFrom(s => s.Weight))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));
            profile.CreateMap<Domain.Model.Recipe, RecipeDetailsVm>();
        }
    }
}
