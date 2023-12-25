using LMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.EF.Repos
{
    public class BookRepo<T> : IBookRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BookRepo(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<T> addNew(T book)
        {
            await _context.Set<T>().AddAsync(book);
            _context.SaveChanges();
            return book;
        }

        public async Task<T> findBook(string keyWord, Expression<Func<T, bool>> predicate)
        {


            var item = _context.Set<T>().Where(predicate).First();
            return item;

        }

   

        public async Task<List<T>> getAll()
        {
            List<T> items = await _context.Set<T>().ToListAsync();
            return items;
        }

        public void remove(T book)
        {
            _context.Set<T>().Remove(book);
            _context.SaveChanges();

        }


        public async Task<T> update(T book, int ISBN)
        {
            var item = await _context.Set<T>().FindAsync(ISBN);
            item = book;
            _context.SaveChanges();
            return item;

        }

    }
}
