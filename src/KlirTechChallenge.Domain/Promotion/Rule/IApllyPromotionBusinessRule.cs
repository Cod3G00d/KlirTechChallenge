using KlirTechChallenge.Domain.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlirTechChallenge.Domain.Promotion.Rule
{
    public interface IApllyPromotionBusinessRule
    {
        void ApplyPromotion(QuoteItemProductData item);

    }
}
