namespace Products.WebApi.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeNow => DateTime.Now;
}
