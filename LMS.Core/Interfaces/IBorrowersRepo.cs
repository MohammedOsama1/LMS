using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces
{
    public interface IBorrowerRepo <T> where T : class
    {
        Task<T> addNew(T Borrower);
        Task<T> findBy( Expression<Func<T,bool>>predicte);
        void remove(T Borrowers);
        Task<T> update(T Borrowers, int Id);
        Task<List<T>> getAll();
    }
}
