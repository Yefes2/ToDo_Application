using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Task = Data.Entities.Task;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task GetById(int id) => _context.Tasks.Find(id);

        public IEnumerable<Task> GetByListId(int listId) =>
            _context.Tasks.Where(t => t.ListId == listId).ToList();

        public IEnumerable<Task> GetAll() => _context.Tasks.ToList();

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
