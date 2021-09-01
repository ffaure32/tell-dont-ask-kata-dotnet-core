using TellDontAsk.Domain;
using TellDontAsk.Service;

namespace TellDontAsk.Tests.Doubles
{
    public class TestShipmentService : IShipmentService
    {
        private Order shippedOrder = null;

        public Order GetShippedOrder() {
            return shippedOrder;
        }

        public void Ship(Order order)
        {
            this.shippedOrder = order;
        }
    }
}