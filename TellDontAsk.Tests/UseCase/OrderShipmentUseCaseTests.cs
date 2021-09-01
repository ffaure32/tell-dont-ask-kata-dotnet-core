using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
{
    public class OrderShipmentUseCaseTests
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly TestShipmentService _shipmentService;
        private readonly OrderShipmentUseCase _useCase;

        public OrderShipmentUseCaseTests()
        {
            _orderRepository = new TestOrderRepository();
            _shipmentService = new TestShipmentService();
            _useCase = new OrderShipmentUseCase(_orderRepository, _shipmentService);
        }

        [Fact]
        public void ShipApprovedOrder()
        {
            var initialOrder = new Order {Id = 1, Status = OrderStatus.Approved};
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest {OrderId = 1};

            _useCase.Run(request);

            Assert.Equal(OrderStatus.Shipped, _orderRepository.GetSavedOrder().Status);
            Assert.Equal(initialOrder, _shipmentService.GetShippedOrder());
        }

        [Fact]
        public void CreatedOrdersCannotBeShipped()
        {
            var initialOrder = new Order {Id = 1, Status = OrderStatus.Created};
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest {OrderId = 1};

            Assert.Throws<OrderCannotBeShippedException>(() => _useCase.Run(request));
        }

        [Fact]
        public void RejectedOrdersCannotBeShipped()
        {
            var initialOrder = new Order {Id = 1, Status = OrderStatus.Rejected};
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest {OrderId = 1};

            Assert.Throws<OrderCannotBeShippedException>(() => _useCase.Run(request));
        }

        [Fact]
        public void ShippedOrdersCannotBeShippedAgain()
        {
            var initialOrder = new Order {Id = 1, Status = OrderStatus.Shipped};
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest {OrderId = 1};

            Assert.Throws<OrderCannotBeShippedTwiceException>(() => _useCase.Run(request));
        }
    }
}