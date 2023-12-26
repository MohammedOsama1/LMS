using LMS.Core.Interfaces;
using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepo<Book> Books { get; }
        IBorrowerRepo<Borrower> Borrowers { get; }

        int Complete();

    }
}
