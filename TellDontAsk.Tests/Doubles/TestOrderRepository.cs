using System.Collections.Generic;
using System.Linq;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class TestOrderRepository : IOrderRepository
    {
        private Order _insertedOrder;
        private readonly IList<Order> _orders = new List<Order>();
        
        public Order GetSavedOrder() {
            return _insertedOrder;
        }

        public void Save(Order order) {
            this._insertedOrder = order;
        }

        public Order GetById(int orderId) {
            return _orders.First(o => o.Id == orderId);
        }

        public void AddOrder(Order order) {
            this._orders.Add(order);
        }

    }
}