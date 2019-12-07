using Microsoft.AspNetCore.Mvc;

namespace INTS_API.Interfaces
{
    public interface IApiTest
    {
        //Testna metoda za testiranje kontrolera
        IActionResult Test();
    }
}
