using BookStore.Proxy.Models;
using Refit;

namespace BookStore.Proxy.Refit;

public interface IBookStoreApiClient
{
    [Get("/api/books")]
    Task<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken);
}
