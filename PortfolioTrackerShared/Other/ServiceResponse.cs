namespace PortfolioTrackerShared.Other;

public class ServiceResponse<T>
{
    public T? Data { get; set; } = default;
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}
