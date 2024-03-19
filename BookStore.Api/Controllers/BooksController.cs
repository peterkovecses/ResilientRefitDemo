using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new Exception("Resiliency test");
    }
}
