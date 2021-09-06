using System;
using System.Collections.Generic;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;

        public OrderCreationUseCase(IOrderRepository orderRepository, IProductCatalog productCatalog)
        {
            this._orderRepository = orderRepository;
            this._productCatalog = productCatalog;
        }

        public void Run(SellItemsRequest request)
        {
            var order = Order.Create();

            foreach (var itemRequest in request.Requests) 
            {
                var product = _productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else
                {
                    order.AddItem(product, itemRequest.Quantity);
                }
            }

            _orderRepository.Save(order);
        }
    }
}