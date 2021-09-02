using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderApprovalUseCase(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        
        public void Run(OrderApprovalRequest request) {
            var order = _orderRepository.GetById(request.OrderId);

            if (order.Status == OrderStatus.Shipped) {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (request.Approved && order.Status == OrderStatus.Rejected) {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!request.Approved && order.Status == OrderStatus.Approved) {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            order.Status = request.Approved ? OrderStatus.Approved : OrderStatus.Rejected;
            _orderRepository.Save(order);
        }

    }
}