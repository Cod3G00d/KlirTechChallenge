using KlirTechChallenge.Domain.Payments;

namespace KlirTechChallenge.Domain.Orders
{
    public interface IOrderStatusWorkflow
    {
        void CalculateOrderStatus(Order order, Payment payment);
    }
}