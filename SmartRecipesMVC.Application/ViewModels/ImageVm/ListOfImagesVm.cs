using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecipesMVC.Application.ViewModels.ImageVm
{
    public class ListOfImagesVm
    {
        public int RecipeId { get; set; }
        [DisplayName("Images")] public List<ImageDetailsVm> Images { get; set; }
        public int Count { get; set; }

    }
}
