using System.Collections.Generic;
using System.ComponentModel;

namespace SmartRecipesMVC.Application.ViewModels.RecipeVm
{
    public class ListRecipeForListVm
    {
        [DisplayName("Przepisy")] public List<RecipeForListVm> Recipes { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
