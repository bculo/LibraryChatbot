using AutoMapper;
using INTS_API.Contracts.BookAPI;
using INTS_API.Entities;
using INTS_API.Filters;
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

        [HttpPost("randombooks")]
        public async Task<IActionResult> GetRandomBooksPost([FromBody] BookRequest model)
        {
            List<Book> books = await _service.GetRandomBooks(model?.Number);
            var bookModles = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(bookModles);
        }

        [HttpGet("randombooks")]
        public async Task<IActionResult> GetRandomBooksGet([FromQuery] int? number)
        {
            List<Book> books = await _service.GetRandomBooks(number);
            var bookModles = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(bookModles);
        }

        [HttpPost("categorybooks")]
        public async Task<IActionResult> GetBooksByCategoryPost([FromBody] BookRequest model)
        {
            List<Book> books = await _service.GetRandomBooksByCategory(model?.Category, model?.Number);
            var bookModles = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(bookModles);
        }

        [HttpGet("categorybooks")]
        public async Task<IActionResult> GetBooksByCategoryGet([FromQuery] BookRequest model)
        {
            List<Book> books = await _service.GetRandomBooksByCategory(model?.Category, model?.Number);
            var bookModles = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(bookModles);
        }

        [HttpPost("reservation")]
        public async Task<IActionResult> SetUserReservation([FromBody] ReservationRequest model)
        {
            var result = await _service.CreateBookReservation(model?.Username, model?.Book);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("userreservations")]
        public async Task<IActionResult> GetUserReservations([FromBody] ReservationRequest model)
        {
            var result = await _service.GetUserReservations(model?.Username);
            List<BookModel> models = _mapper.Map<List<BookModel>>(result);
            return Ok(models);
        }

        [HttpPost("bookrating")]
        [ValidateModel]
        public async Task<IActionResult> SetBookRating([FromBody] ReservationRequest model)
        {
            var result = await _service.SetBookReservation(model?.Username, model?.Book, model?.Rating);

            BookRatingResponse rating = new BookRatingResponse()
            {
                Result = result
            };

            return Ok(rating);
        }

        public override string GetControllerName()
        {
            return nameof(BookController);
        }
    }
}