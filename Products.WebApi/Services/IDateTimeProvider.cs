namespace Products.WebApi.Services;

public interface IDateTimeProvider
{
    public DateTime DateTimeNow { get; }
}
