using LMS.Core;
using LMS.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LMS.Api.Controllers
{
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork ;

        public BorrowerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ;
        }

        [HttpGet("getAllBorrowers")]
        public async Task<IActionResult> getAllBorrowers() 
        {
            List <Borrower> data = await _unitOfWork.Borrowers.getAll();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("assginNewBorrower")]
        public async Task<IActionResult> signNewBorrower ([FromForm] Borrower borrower)
        {
           var item = await _unitOfWork.Borrowers.findBy(x => x.Email == borrower.Email);
           if (item == null)
            {
                await _unitOfWork.Borrowers.addNew(borrower);
                _unitOfWork.Complete();
                return Ok();
            }
            else
            {
                return BadRequest("Already exist");
            }

        }

        [HttpPost("removeBorrower")]
        public async Task<IActionResult> removeBorrower(int id)
        {
            var item = await _unitOfWork.Borrowers.findBy(x=>x.Id == id);

             if(item == null)
            {
                return BadRequest("user Dosnt exist");

            }
             _unitOfWork.Borrowers.remove(item);
            _unitOfWork.Complete();
             return Ok("Borrower Removed Succes");


        }

        [HttpPost("updateBorrowers")]
        public async Task <IActionResult> updateBorrowerDetails(int id , string? name ,string ? email) 
        {
            var item = await _unitOfWork.Borrowers.findBy(x => x.Id == id);

            if (item == null)
            {
                return BadRequest("user Dosnt exist");

            }
            item.Name = name ?? item.Name;
            item.Email = email ?? item.Email;
            _unitOfWork.Complete();
            return Ok(":)");
        }

    }
}
