using LMS.Core;
using LMS.Core.Models;
using LMS.EF;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
      private readonly  IUnitOfWork _unitOfWork;
      public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getAllBooks")]
        public async Task<IActionResult> getAllBooks() 
        {
            var books = await _unitOfWork.Books.getAll();
            if (books == null)
            {
                return Ok("your Book Shelf is Empty");
            }
            else
            {
                return Ok(books);
            }
           
        }

        [HttpPost("addNewBook")]
        public async Task<IActionResult> addNewBook([FromForm] Book book)
        {
            var  adder = await _unitOfWork.Books.addNew(book);
            return Ok(adder);
        }

        [HttpPost("updateBook")]
        public async Task<IActionResult> UpdateBook([FromForm] int id, string? title, string? shelfLocation, string? availableQuantity, string? author)
        {
            Book book = await _unitOfWork.Books.findBookById(id);

            if (book == null)
            {
                return NotFound(); // Book not found
            }

            book.Title = title ?? book.Title;

            if (int.TryParse(shelfLocation, out int parsedShelfLocation))
            {
                book.ShelfLocation = parsedShelfLocation;
            }

            book.Author = author ?? book.Author;

            if (int.TryParse(availableQuantity, out int parsedAvailableQuantity))
            {
                book.AvailableQuantity = parsedAvailableQuantity;
            }

            _unitOfWork.Complete();

            return Ok(book);
        }

        [HttpPost("deleteBook")]
        public async Task<IActionResult> UpdateBook([FromForm] int id )
        {
            Book book = await _unitOfWork.Books.findBookById(id);

            if (book == null)
            {
                return NotFound(); // Book not found
            }


            _unitOfWork.Books.remove(book);
            return Ok("Book removed Succesfully");
        }

    }
}