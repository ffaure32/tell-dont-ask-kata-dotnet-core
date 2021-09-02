using TellDontAsk.Domain;
using TellDontAsk.Service;

namespace TellDontAsk.Tests.Doubles
{
    public class TestShipmentService : IShipmentService
    {
        private Order _shippedOrder;

        public Order GetShippedOrder() {
            return _shippedOrder;
        }

        public void Ship(Order order)
        {
            this._shippedOrder = order;
        }
    }
}