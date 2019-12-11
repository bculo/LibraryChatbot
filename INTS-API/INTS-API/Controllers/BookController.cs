using AutoMapper;
using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Models.BookAPI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : LibraryMainController
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BookController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("filldata")]
        public async Task<IActionResult> AddBook(BookModel book)
        {
            try
            {
                var newBook = _mapper.Map<Book>(book);
                bool success = await _service.AddBokk(newBook);

                if(success)
                    return Ok();

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("randombooks")]
        public async Task<IActionResult> GetRandomBooks()
        {
            List<Book> books = await _service.GetRandomBooks();
            var bookModles = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(bookModles);
        }

        public override string GetControllerName()
        {
            return nameof(BookController);
        }
    }
}