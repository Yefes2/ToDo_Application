using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IListService
    {
        List GetListById(int id);
        IEnumerable<List> GetListsByUserId(int userId);
        void CreateList(int userId, string name);
        void RenameList(int listId, string newName);
        void DeleteList(int listId);
    }
}
