using FitUpAppBackend.Core.Common.Services;

namespace FitUpAppBackend.Infrastructure.Common.Services;

public class DateService : IDateService
{
    public DateTimeOffset CurrentOffsetDate()
     => DateTimeOffset.Now;

    public DateTime CurrentDate()
    => DateTime.Now;
}