﻿using TellDontAsk.Domain;
using TellDontAsk.Repository;
using TellDontAsk.Service;

namespace TellDontAsk.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShipmentService shipmentService;

        public OrderShipmentUseCase(IOrderRepository orderRepository, IShipmentService shipmentService)
        {
            this.orderRepository = orderRepository;
            this.shipmentService = shipmentService;
        }
        
        public void Run(OrderShipmentRequest request) {
            var order = orderRepository.GetById(request.OrderId);

            if (order.Status == OrderStatus.Created || order.Status == OrderStatus.Rejected) {
                throw new OrderCannotBeShippedException();
            }

            if (order.Status == OrderStatus.Shipped) {
                throw new OrderCannotBeShippedTwiceException();
            }

            shipmentService.Ship(order);

            order.Status = OrderStatus.Shipped;
            orderRepository.Save(order);
        }

    }
}