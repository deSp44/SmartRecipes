using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
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

        public int NumberOfIngredients
        {
            get => RecipeIngredients.Count;
        }

        public int Id { get; set; }
        public string OwnerId { get; set; }
        [DisplayName("Name")] public string Name { get; set; }
        [DisplayName("Description")] public string Description { get; set; }
        [DisplayName("Create date")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        [DisplayName("Preparation time")] public short PreparationTime { get; set; }
        [DisplayName("Portions")] public short Portions { get; set; }
        [DisplayName("Preparation")] public string Preparation { get; set; }
        [DisplayName("Hints")] public string Hints { get; set; }
        [DisplayName("Difficulty")] public string Difficulty { get; set; }
        public bool IsActive { get; set; }

        [DisplayName("Ingredients")] public IList<RecipeIngredient> RecipeIngredients { get; set; }
        [DisplayName("Add image")] public IList<Image> Images { get; set; }
        //[DisplayName("Tagi")] public IList<TagsForListForRecipeDetailsVm> RecipeTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRecipeVm, Domain.Model.Recipe>().ReverseMap();
            profile.CreateMap<IngredientsDetailsVm, Domain.Model.Connections.RecipeIngredient>().ReverseMap();
        }
    }

    public class NewRecipeValidation : AbstractValidator<NewRecipeVm>
    {
        public NewRecipeValidation()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().MaximumLength(255);
            RuleFor(x => x.Description).NotNull().MaximumLength(255);
            RuleFor(x => x.CreateDate).NotNull();
            RuleFor(x => x.PreparationTime).NotNull().GreaterThanOrEqualTo((short)1);
            RuleFor(x => x.Portions).NotNull().GreaterThanOrEqualTo((short)1);
            RuleFor(x => x.Preparation).NotNull();
            RuleFor(x => x.Difficulty).NotNull();

            RuleForEach(x => x.RecipeIngredients)
                .ChildRules(ingredient =>
                {
                    ingredient.RuleFor(x => x.Ingredient.Name).NotNull().NotEmpty();

                    ingredient.RuleFor(x => x.Weight).GreaterThan(0);
                    ingredient.RuleFor(x => x.Weight).Null().When(y => y.Quantity.HasValue);
                    ingredient.RuleFor(x => x.Weight).NotEmpty().When(y => y.Quantity is null);
                    
                    ingredient.RuleFor(x => x.Quantity).GreaterThan(0);
                    ingredient.RuleFor(x => x.Quantity).Null().When(y => y.Weight.HasValue);
                    ingredient.RuleFor(x => x.Quantity).NotEmpty().When(y => y.Weight is null);
                });
        }
    }
}
