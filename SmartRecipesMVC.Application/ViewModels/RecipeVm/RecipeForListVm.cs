using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SmartRecipesMVC.Application.Mapping;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class RecipeForListVm : IMapFrom<Domain.Model.Recipe>
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")] public string Name { get; set; }
        [DisplayName("Opis")] public string Description { get; set; }
        [DisplayName("Data utworzenia")] [DataType(DataType.Date)] public DateTime CreateDate { get; set; }
        public string ImagePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Recipe, RecipeForListVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.ToUpper()))
                .ForMember(d => d.ImagePath, opt => opt.MapFrom(s => s.ImagePath))
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate));
        }
    }
}
