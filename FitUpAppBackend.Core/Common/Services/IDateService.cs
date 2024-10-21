
namespace FitUpAppBackend.Core.Common.Services;

public interface IDateService
{
    DateTimeOffset CurrentDateTime();
    DateTimeOffset CurrentDate();
}