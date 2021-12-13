using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.SharedKernel;
using System.Collections.Generic;

namespace KlirTechChallenge.Domain.Promotions;

public class Promotion : AggregateRoot<PromotionId>
{
    public string Name { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreationDate { get; }


    public static Promotion CreateNew(string name, bool active)
    {
        return new Promotion(PromotionId.Of(Guid.NewGuid()), name, active);
    }

    public void  ChangeItem(bool active)
    {
        Active = active;
        if (!Active)
        {
            Name = "";
        }
    }

    

    private Promotion(PromotionId id, string name, bool active)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));

        Id = id;
        Name = name;
        Active = active;
        CreationDate = DateTime.Now;
    }

    // Empty constructor for EF
    private Promotion(){}
}