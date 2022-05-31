using Lion.Core.Application._Common.Interfaces;

namespace Lion.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}