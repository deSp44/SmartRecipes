using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class NewRecipeVm
    {
        // TODO : REFACTOR TO CORRECT PROPERTIES
        public int Id { get; set; }
        [DisplayName("Nazwa")] public string Name { get; set; }
        [DisplayName("Opis")] public string Description { get; set; }
        [DisplayName("Data utworzenia")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        [DisplayName("Czas przygotowania")] public short PreparationTime { get; set; }
        [DisplayName("Porcje")] public short Portions { get; set; }
        [DisplayName("Przygotowanie")] public string Preparation { get; set; }
        [DisplayName("Wskazówki")] public string Hints { get; set; }

        [DisplayName("Trudność przygotowania")] public Difficulty Difficulty { get; set; }

        [DisplayName("Składniki")] public IList<IngredientsForListForRecipeDetailsVm> RecipeIngredients { get; set; }
        [DisplayName("Zdjęcia")] public IList<Image> Images { get; set; }
        //[DisplayName("Tagi")] public IList<TagsForListForRecipeDetailsVm> RecipeTags { get; set; }


        // public void Mapping(Profile profile)
        // {
        //     profile.CreateMap<Domain.Model.Connections.RecipeIngredient, IngredientsForListForRecipeDetailsVm>()
        //         .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Ingredient.Id))
        //         .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Ingredient.Name))
        //         .ForMember(d => d.Weight, opt => opt.MapFrom(s => s.Weight))
        //         .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));
        //
        //     profile.CreateMap<Domain.Model.Recipe, RecipeDetailsVm>();
        // }
    }
}
