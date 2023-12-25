
using System.Linq.Expressions;

namespace LMS.Core.Interfaces
{
    public interface IBookRepo<T> where T : class
    {
        Task<T> addNew(T book);
        void remove(T book);
        Task<T> update(T book, int ISBN);
        Task<List<T>> getAll();
        Task<T> findBook(Expression<Func<T, bool>> predicate);
        Task<T> findBookById(int id);

    }
}
