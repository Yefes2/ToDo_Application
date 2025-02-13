using Business.Interfaces;
using Data.Entities;
using DataAccess.Repositories;
using System.Collections.Generic;

namespace Business.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public List GetListById(int id) => _listRepository.GetById(id);

        public IEnumerable<List> GetListsByUserId(int userId) => _listRepository.GetByUserId(userId);

        public void CreateList(int userId, string name)
        {
            var newList = new List { Name = name, UserId = userId };
            _listRepository.Add(newList);
        }

        public void RenameList(int listId, string newName)
        {
            var list = _listRepository.GetById(listId);
            if (list != null)
            {
                list.Name = newName;
                _listRepository.Update(list);
            }
        }

        public void DeleteList(int listId)
        {
            _listRepository.Delete(listId);
        }
    }
}
