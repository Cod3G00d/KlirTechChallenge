using System.ComponentModel.DataAnnotations;

namespace KlirTechChallenge.Application.Customers.UpdateCustomer
{
    public  class UpdateCustomerRequest
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 5)]
        public string Name { get; init; }
    }
}
