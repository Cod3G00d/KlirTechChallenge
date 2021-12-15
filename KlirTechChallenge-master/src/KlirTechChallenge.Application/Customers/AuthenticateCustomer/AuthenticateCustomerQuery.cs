using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;

namespace KlirTechChallenge.Application.Customers.AuthenticateCustomer
{
    public  class AuthenticateCustomerQuery : Query<CustomerViewModel>
    {
        public string Email { get; init; }
        public string Password { get; init; }

        public AuthenticateCustomerQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override ValidationResult Validate()
        {
            return new AuthenticateCustomerQueryValidator().Validate(this);
        }
    }

    public class AuthenticateCustomerQueryValidator : AbstractValidator<AuthenticateCustomerQuery>
    {
        public AuthenticateCustomerQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is empty.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Password is empty.");
        }
    }
}