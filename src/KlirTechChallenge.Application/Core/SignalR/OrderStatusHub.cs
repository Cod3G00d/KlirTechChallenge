using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using KlirTechChallenge.Application.Orders.GetOrderDetails;

namespace KlirTechChallenge.Application.Core.SignalR;

public interface IOrderStatusHubClient
{
    Task UpdateOrderStatus(string orderId, OrderStatusViewModel orderStatus);
}

public class OrderStatusHub : Hub<IOrderStatusHubClient>
{
    public Task JoinCustomerToGroup(string customerId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, customerId);
    }
}    

