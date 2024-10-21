using FitUpAppBackend.Core.Common.Services;

namespace FitUpAppBackend.Infrastructure.Common.Services;

public class DateService : IDateService
{
    public DateTimeOffset CurrentDateTime() 
        => DateTimeOffset.Now.DateTime;

    public DateTimeOffset CurrentDate() 
        => DateTimeOffset.Now.Date;
}