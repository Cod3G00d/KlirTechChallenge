using MediatR;
using System;
using FluentValidation;
using FluentValidation.Results;

namespace KlirTechChallenge.Application.Core.CQRS.QueryHandling
{
    /// <summary>
    /// Interface for Query implementation
    /// </summary>
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        public abstract ValidationResult Validate();
    }

    /// <summary>
    /// Abstract class to be inherited by Queries
    /// </summary>
    public abstract  class Query<TResult> : IQuery<QueryHandlerResult<TResult>>
    {
        public ValidationResult ValidationResult { get; init; }

        public virtual ValidationResult Validate()
        {
            return ValidationResult;
        }
    }
}