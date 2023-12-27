using LMS.Core;
using LMS.Core.Models;
using LMS.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace LMS.Api.Controllers
{
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IUnitOfWork _unitOfWork;
        public BorrowingController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {                          
            _context = context;
            _unitOfWork = unitOfWork;

        }

        [HttpPost("checkout")]
        public async Task<IActionResult> checkout (int bookId , int borrowerId,int day) 
        {
            Book book =  await _unitOfWork.Books.findBookById(bookId);
            Borrower borrower = await _unitOfWork.Borrowers.findBy(x => x.Id == borrowerId);

            if (book != null && borrower != null) 
            {
                if (await _context.BorrowingRecords.FirstOrDefaultAsync(x => x.BookId == bookId && x.BorrowerId == borrowerId) == null)
                {
                    if (book.AvailableQuantity > 0)
                    {
                        BorrowingRecord borrowingRecord = new BorrowingRecord() { BookId = bookId, BorrowerId = borrowerId, DueDate = DateTime.Now.AddDays(day) };
                        await _context.BorrowingRecords.AddAsync(borrowingRecord);
                        book.AvailableQuantity = book.AvailableQuantity - 1;
                        _context.SaveChanges();
                        return Ok("Book Assigned Sucssefully : )");

                    }
                    else
                    {
                        return BadRequest("this book is not avaaliable now :(");
                    }
                }
                else
                {
                    return BadRequest("this borrower alredy have this book");
                }
                
            }
            else { return BadRequest("user or book dosnt exist"); }

        }

        [HttpPost("returnBook")]
        public async Task<IActionResult> returnBook(int bookId, int borrowerId)
        {
            Book book = await _unitOfWork.Books.findBookById(bookId);
            Borrower borrower = await _unitOfWork.Borrowers.findBy(x => x.Id == borrowerId);

            if (book != null && borrower != null)
            {
                BorrowingRecord borrowingRecord = await _context.BorrowingRecords.FirstOrDefaultAsync(x => x.BookId == bookId && x.BorrowerId == borrowerId);
                if (borrowingRecord != null)
                {
                    borrowingRecord.ReturnDate = DateTime.Now;
                    book.AvailableQuantity = book.AvailableQuantity + 1;
                    _context.SaveChanges();
                    return Ok("Book returned successfully : )");
                }
                else
                {
                    return BadRequest("This payment record does not exist");
                }
            }
            else
            {
                return BadRequest("User or book doesn't exist");
            }
        }

        [HttpPost("getMyBooks")]
        public async Task<IActionResult> getMyBooks(string email)
        {
            Borrower borrower = await _unitOfWork.Borrowers.findBy(x => x.Email == email);

            if (borrower != null)
            {
                var borrowingRecords = await _context.BorrowingRecords
                    .Include(record => record.Book)
                    .Where(record => record.BorrowerId == borrower.Id && record.ReturnDate == DateTime.MinValue)
                    .Select(record => new
                    {
                        BookId = record.BookId,
                        Title = record.Book.Title,
                        Author = record.Book.Author,
                        // Include other book properties you want to expose
                        CheckOutDate = record.CheckOutDate,
                        DueDate = record.DueDate,
                        ReturnDate = record.ReturnDate
                    })
                    .ToListAsync();

                return Ok(borrowingRecords);
            }
            else
            {
                return BadRequest("User doesn't have Books yet");
            }
        }

    }
}
