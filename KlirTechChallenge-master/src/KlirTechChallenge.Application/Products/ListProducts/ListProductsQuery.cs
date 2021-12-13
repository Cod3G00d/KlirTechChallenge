using System.Collections.Generic;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;

namespace KlirTechChallenge.Application.Products.ListProducts;

public record class ListProductsQuery : Query<IList<ProductViewModel>>
{
    public string Currency { get; init;  }

    public ListProductsQuery(string currency)
    {
        Currency = currency;
    }

    public override ValidationResult Validate()
    {
        return new ListProductsQueryValidator().Validate(this);
    }
}

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is empty.");
    }
}
