using AutoMapper;
using KlirTechChallenge.Application.Customers.RegisterCustomer;

namespace KlirTechChallenge.Application.Core.AutoMapper;

public class RequestToCommandProfile : Profile
{
    public RequestToCommandProfile()
    {
        CreateMap<RegisterCustomerRequest, RegisterCustomerCommand>()
        .ConstructUsing(c => new RegisterCustomerCommand(c.Email, c.Name, c.Password, c.PasswordConfirm));
    }
}
