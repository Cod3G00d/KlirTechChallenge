using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using KlirTechChallenge.Application.Promotions;

namespace KlirTechChallenge.Application.Promotion.Promotions
{
    public class ListPromotionsQuery : Query<IList<PromotionsViewModel>>
    {

        public ListPromotionsQuery()
        {

        }

        public override ValidationResult Validate()
        {
            return new ListPromotionsQueryValidator().Validate(this);
        }
    }

    public class ListPromotionsQueryValidator : AbstractValidator<ListPromotionsQuery>
    {
        public ListPromotionsQueryValidator()
        {
            // RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is empty.");
        }
    }
}