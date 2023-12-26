using LMS.Core.Interfaces;
using LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.EF.Repos
{
    public class BorrowerRepo <T> : IBorrowerRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BorrowerRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> addNew(T borrowers)
        {
            await _context.Set<T>().AddAsync(borrowers);
            return borrowers;
        }

        public async Task<T> findBy(Expression<Func<T,bool>>predicte)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicte);
        }

        public async Task<List<T>> getAll()
        {
            List<T> allValues = await _context.Set<T>().ToListAsync();
            return allValues;
        }

        public void remove(T borrowers)
        {
            _context.Set<T>().Remove(borrowers);
            _context.SaveChanges();
        }

        public async Task<T> update(T borrower, int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            item = borrower;
            return item;
            

        }
    }
}
