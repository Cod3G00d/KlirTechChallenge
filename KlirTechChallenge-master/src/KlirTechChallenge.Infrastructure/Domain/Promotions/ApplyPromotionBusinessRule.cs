using KlirTechChallenge.Domain.Promotion.Rule;
using KlirTechChallenge.Domain.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlirTechChallenge.Infrastructure.Domain.Promotions
{
    public class ApplyPromotionBusinessRule : IApllyPromotionBusinessRule
    {

        public string FromCurrency { get; private set; }
        public string ToCurrency { get; private set; }
        public decimal ConversionRate { get; private set; }

        public ApplyPromotionBusinessRule()
        {

        }

        public void ApplyPromotion(QuoteItemProductData item)
        {
            
            switch (item.PromotionName)
            {
                case "Buy 1 Get 1 Free":
                    item.TotalPrice = item.ProductPrice.Value * item.Quantity;
                    item.Quantity = item.Quantity * 2;
                    break;

                case "3 for 10 Euro":
                    if (item.Quantity >= 3)
                    {
                        var inteiro = item.Quantity / 3;
                        var quociente = item.Quantity % 3;

                        item.TotalPrice = (inteiro * 10) + (quociente * item.ProductPrice.Value);
                    }else
                    {
                        item.TotalPrice = item.Quantity * item.ProductPrice.Value;
                    }
                    break;
                default:
                    item.TotalPrice = item.Quantity * item.ProductPrice.Value;
                    break; ;
            }
        }
    }
}
