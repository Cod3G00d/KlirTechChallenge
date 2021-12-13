using System;

namespace KlirTechChallenge.Domain.SeedWork;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}