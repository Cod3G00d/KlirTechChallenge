using System;
using System.ComponentModel.DataAnnotations;

namespace KlirTechChallenge.Application.Orders.GetOrderDetails
{
    public  class GetOrderDetailsRequest
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        public Guid OrderId { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Currency { get; init; }
    }
}
