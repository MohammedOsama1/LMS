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

        [HttpPost("search4Book")]
        public async Task<IActionResult> search4Book([FromForm] string kewWord )
        {
            Book? filter1 = await _unitOfWork.Books.findBook(x=>x.Title == kewWord);
            Book? filter2 = await _unitOfWork.Books.findBook(x => x.Author  == kewWord);
            Book? filter3 = await _unitOfWork.Books.findBook(x => x.ISBN == kewWord);

            if (filter1 == null)
            {
                if (filter2 == null)
                {
                    if (filter3 == null)
                    {
                        return NotFound();
                    }
                }
            }

            return Ok(filter1 ?? filter2 ?? filter3);
        }


        [HttpPost("deleteBook")]
        public async Task<IActionResult> deleteBook([FromForm] int id)
        {
            Book book = await _unitOfWork.Books.findBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.Books.remove(book);
            return Ok("Book removed Succesfully");
        }

    }
}