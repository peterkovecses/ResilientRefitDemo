using BookStore.Proxy.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BookStore.Proxy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookStoreApiClient bookStoreApiClient) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellationToken) 
        => Ok(await bookStoreApiClient.GetBooksAsync(cancellationToken));
}
