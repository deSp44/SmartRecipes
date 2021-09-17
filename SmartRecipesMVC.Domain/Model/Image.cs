using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecipesMVC.Domain.Model
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ext { get; set; }
        public string ImagePath { get; set; }
        public bool IsMainImage { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
