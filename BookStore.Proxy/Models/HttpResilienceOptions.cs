namespace BookStore.Proxy.Models;

public class HttpResilienceOptions
{
    public int MaxRetryAttempts { get; set; }
    public TimeSpan RetryDelay { get; set; }
    public TimeSpan AttemptTimeout { get; set; }
    public TimeSpan TotalRequestTimeout { get; set; }
}
