using Microsoft.AspNetCore.Mvc;

namespace INTS_API.Interfaces
{
    public interface IApiTest
    {
        IActionResult Test();
        string GetControllerName();
    }
}
