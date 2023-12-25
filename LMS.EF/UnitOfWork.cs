using LMS.Core;
using LMS.Core.Interfaces;
using LMS.Core.Models;
using LMS.EF.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LMS.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepo<Book> Books { get; private set; }


        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BookRepo<Book>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

