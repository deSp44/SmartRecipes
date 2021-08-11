using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Domain.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short DifficultyId { get; set; }
        public short PreparationTime { get; set; }
        public short Portions { get; set; }
        public string Preparation { get; set; }
        public string Hints { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Difficulty Difficulty { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
