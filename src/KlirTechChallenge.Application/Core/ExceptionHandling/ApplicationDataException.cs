using System;

namespace KlirTechChallenge.Application.Core.ExceptionHandling;

public class ApplicationDataException : Exception
{
    public ApplicationDataException(string message) : base(message) { }
}