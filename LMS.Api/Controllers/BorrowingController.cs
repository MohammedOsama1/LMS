using LMS.Core;
using LMS.Core.Models;
using LMS.EF;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> checkout (int bookId , int borrowerId) 
        {
            Book book =  await _unitOfWork.Books.findBookById(bookId);
            Borrower borrower = await _unitOfWork.Borrowers.findBy(x => x.Id == borrowerId);

            if (book != null && borrower != null) 
            {
                if(book.AvailableQuantity>0)
                {
                    BorrowingRecord borrowingRecord = new BorrowingRecord() { BookId = bookId, BorrowerId = borrowerId };
                    await _context.BorrowingRecords.AddAsync(borrowingRecord);
                    book.AvailableQuantity = book.AvailableQuantity-1;
                    _context.SaveChanges();
                    return Ok("Book Assigned Sucssefully : )");

                }
                else
                {
                    return BadRequest("this book is not avaaliable now :(");
                }

                
            }
            else { return BadRequest("user or book dosnt exist"); }

        }
    }
}
