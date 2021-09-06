using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Domain.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Difficulty { get; set; }
        public short PreparationTime { get; set; }
        public short Portions { get; set; }
        public string Preparation { get; set; }
        public string Hints { get; set; }
        public bool IsActive { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
