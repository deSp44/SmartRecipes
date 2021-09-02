using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartRecipesMVC.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
    }
}
