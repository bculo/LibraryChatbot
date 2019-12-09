using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Models.BookAPI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace INTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : LibraryMainController
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost("filldata")]
        public async Task<IActionResult> AddBook(BookModel book)
        {
            var newBook = new Book()
            {
                Authors = book.Authors,
                AvarageRating = book.AvarageRating,
                ISBN = book.ISBN,
                ISBN13 = book.ISBN13,
                LanguageCode = book.LanguageCode,
                PageNumber = book.PageNumber,
                Title = book.Title,
                RatingsCount = book.RatingsCount,
            };

            await _service.AddBokk(newBook);
            return Ok();
        }

        public override string GetControllerName()
        {
            return nameof(BookController);
        }
    }
}