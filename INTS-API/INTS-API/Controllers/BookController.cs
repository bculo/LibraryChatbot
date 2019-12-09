using Microsoft.AspNetCore.Mvc;

namespace INTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : LibraryMainController
    {
        public override string GetControllerName()
        {
            return nameof(BookController);
        }
    }
}