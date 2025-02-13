using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IListRepository
    {
        List GetById(int id);
        IEnumerable<List> GetByUserId(int userId);
        IEnumerable<List> GetAll();
        void Add(List list);
        void Update(List list);
        void Delete(int id);
    }
}
