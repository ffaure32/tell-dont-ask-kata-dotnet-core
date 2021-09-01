using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
{
    public class OrderApprovalUseCaseTests
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly OrderApprovalUseCase _useCase;

        public OrderApprovalUseCaseTests()
        {
            _orderRepository = new TestOrderRepository();
            _useCase = new OrderApprovalUseCase(_orderRepository);
        }
        
        [Fact]
        public void ApprovedExistingOrder()
        {
            var initialOrder = new Order {Status = OrderStatus.Created, Id = 1};
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest {OrderId = 1, Approved = true};

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Approved, savedOrder.Status);
        }

        [Fact]
        public void RejectedExistingOrder()
        {
            var initialOrder = new Order {Status = OrderStatus.Created, Id = 1};
            _orderRepository.AddOrder(initialOrder);
            var request = new OrderApprovalRequest {OrderId = 1, Approved = false};

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Rejected, savedOrder.Status);
        }

        [Fact]
        public void CannotApproveRejectedOrder()
        {
            var initialOrder = new Order {Status = OrderStatus.Rejected, Id = 1};
            _orderRepository.AddOrder(initialOrder);
            var request = new OrderApprovalRequest {OrderId = 1, Approved = true};

            Assert.Throws<RejectedOrderCannotBeApprovedException>(() => _useCase.Run(request));
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            var initialOrder = new Order {Status = OrderStatus.Approved, Id = 1};
            _orderRepository.AddOrder(initialOrder);
            var request = new OrderApprovalRequest {OrderId = 1, Approved = false};

            Assert.Throws<ApprovedOrderCannotBeRejectedException>(() => _useCase.Run(request));
        }

        [Fact]
        public void ShippedOrdersCannotBeApproved()
        {
            var initialOrder = new Order {Status = OrderStatus.Shipped, Id = 1};
            _orderRepository.AddOrder(initialOrder);
            var request = new OrderApprovalRequest {OrderId = 1, Approved = true};

            Assert.Throws<ShippedOrdersCannotBeChangedException>(() => _useCase.Run(request));
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            var initialOrder = new Order {Status = OrderStatus.Shipped, Id = 1};
            _orderRepository.AddOrder(initialOrder);
            var request = new OrderApprovalRequest {OrderId = 1, Approved = false};

            Assert.Throws<ShippedOrdersCannotBeChangedException>(() => _useCase.Run(request));
        }
    }
}