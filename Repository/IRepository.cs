using System.Collections.Generic;
using NetCore_Dapper.Models;

namespace NetCore_Dapper.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}