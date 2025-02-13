using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Task = Data.Entities.Task;

namespace DataAccess.Repositories
{
    public interface ITaskRepository
    {
        Task GetById(int id);
        IEnumerable<Task> GetByListId(int listId);
        IEnumerable<Task> GetAll();
        void Add(Task task);
        void Update(Task task);
        void Delete(int id);
    }
}
