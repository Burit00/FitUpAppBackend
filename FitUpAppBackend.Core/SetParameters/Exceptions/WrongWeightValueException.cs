using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.SetParameters.Exceptions;

public sealed class WrongWeightValueException() : FitUpException("Wrong Weight Value");