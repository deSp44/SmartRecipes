using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Domain.Model;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class NewRecipeVm : IMapFrom<Domain.Model.Recipe>
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

        //[DisplayName("Trudność przygotowania")] public Difficulty Difficulty { get; set; }

        //[DisplayName("Składniki")] public IList<IngredientsForListForRecipeDetailsVm> RecipeIngredients { get; set; }
        //[DisplayName("Zdjęcia")] public IList<Image> Images { get; set; }
        //[DisplayName("Tagi")] public IList<TagsForListForRecipeDetailsVm> RecipeTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRecipeVm, Domain.Model.Recipe>();

        }
    }

    public class NewRecipeValidation : AbstractValidator<NewRecipeVm>
    {
        public NewRecipeValidation()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(255);
        }
    }
}
