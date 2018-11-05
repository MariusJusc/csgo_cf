using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T Entity);

        void Update(T Entity);

        void Delete(T Entity);

        void Delete(int id);

        void Detach(T Entity);

    }
}
