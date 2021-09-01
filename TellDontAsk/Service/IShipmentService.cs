using TellDontAsk.Domain;

namespace TellDontAsk.Service
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}