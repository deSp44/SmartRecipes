using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartRecipesMVC.Application.Interfaces;
using SmartRecipesMVC.Domain.Interface;

namespace SmartRecipesMVC.Application.Services
{
    public class TrashService : ITrashService
    {
        private readonly ITrashRepository _trashRepository;
        private readonly IMapper _mapper;

        public TrashService(ITrashRepository trashRepository, IMapper mapper)
        {
            _trashRepository = trashRepository;
            _mapper = mapper;
        }

        public void DeleteRecipe(int id)
        {
            _trashRepository.DeleteRecipe(id);
        }

        public void RestoreRecipe(int id)
        {
            _trashRepository.RestoreRecipe(id);
        }
    }
}
