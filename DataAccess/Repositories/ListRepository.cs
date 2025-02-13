using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly ApplicationDbContext _context;

        public ListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List GetById(int id) => _context.Lists.Find(id);

        public IEnumerable<List> GetByUserId(int userId) =>
            _context.Lists.Where(l => l.UserId == userId).ToList();

        public IEnumerable<List> GetAll() => _context.Lists.ToList();

        public void Add(List list)
        {
            _context.Lists.Add(list);
            _context.SaveChanges();
        }

        public void Update(List list)
        {
            _context.Lists.Update(list);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var list = _context.Lists.Find(id);
            if (list != null)
            {
                _context.Lists.Remove(list);
                _context.SaveChanges();
            }
        }
    }
}