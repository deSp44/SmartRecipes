﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Domain.Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
