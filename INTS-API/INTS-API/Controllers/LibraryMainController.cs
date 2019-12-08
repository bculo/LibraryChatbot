using INTS_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTS_API.Controllers
{
    public abstract class LibraryMainController : ControllerBase, IApiTest
    {
        public abstract string GetControllerName();

        /// <summary>
        /// Testna metoda za provjeru API-a
        /// </summary>
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(GetControllerName());
        }
    }
}
