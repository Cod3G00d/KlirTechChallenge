namespace KlirTechChallenge.Application.Promotions;

public record PromotionsViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool Active { get; init; }

}