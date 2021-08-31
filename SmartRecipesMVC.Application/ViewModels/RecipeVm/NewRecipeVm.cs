using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using SmartRecipesMVC.Application.Mapping;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class NewRecipeVm : IMapFrom<Domain.Model.Recipe>
    {
        public NewRecipeVm()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }

        public int Id { get; set; }
        [DisplayName("Name")] public string Name { get; set; }
        [DisplayName("Description")] public string Description { get; set; }
        [DisplayName("Create date")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        [DisplayName("Preparation time")] public short PreparationTime { get; set; }
        [DisplayName("Portions")] public short Portions { get; set; }
        [DisplayName("Preparation")] public string Preparation { get; set; }
        [DisplayName("Hints")] public string Hints { get; set; }
        [DisplayName("Difficulty")] public short DifficultyId { get; set; }
        public bool IsActive { get; set; }

        [DisplayName("Ingredients")] public IList<RecipeIngredient> RecipeIngredients { get; set; }
        //[DisplayName("Zdjęcia")] public IList<Image> Images { get; set; }
        //[DisplayName("Tagi")] public IList<TagsForListForRecipeDetailsVm> RecipeTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRecipeVm, Domain.Model.Recipe>().ReverseMap();
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
