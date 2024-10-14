namespace FitUpAppBackend.Core.Common.Services;

public interface IDateService
{
    DateTimeOffset CurrentOffsetDate();
    DateTime CurrentDate();
}