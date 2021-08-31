using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecipesMVC.Application.Interfaces
{
    public interface ITrashService
    {
        void DeleteRecipe(int id);
        void RestoreRecipe(int id);
    }
}
