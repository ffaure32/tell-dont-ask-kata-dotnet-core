using System.Collections.Generic;
using System.Linq;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class TestOrderRepository : IOrderRepository
    {
        private Order insertedOrder;
        private IList<Order> orders = new List<Order>();
        
        public Order GetSavedOrder() {
            return insertedOrder;
        }

        public void Save(Order order) {
            this.insertedOrder = order;
        }

        public Order GetById(int orderId) {
            return orders.First(o => o.Id == orderId);
        }

        public void AddOrder(Order order) {
            this.orders.Add(order);
        }

    }
}