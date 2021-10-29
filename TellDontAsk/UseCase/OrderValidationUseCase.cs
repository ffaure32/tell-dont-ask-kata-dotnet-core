using System.Linq;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Repository;
using TellDontAsk.Service;

namespace TellDontAsk.UseCase
{
    public class OrderValidationUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogService _catalogService;
        private readonly IStockService _stockService;

        public OrderValidationUseCase(IOrderRepository orderRepository,
            ICatalogService catalogService, IStockService stockService)
        {
            _orderRepository = orderRepository;
            _catalogService = catalogService;
            _stockService = stockService;
        }

        public async Task Run(OrderValidationRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);

            bool catalogVerified = await _catalogService.CheckCatalog(order);
            bool stockAvailable = await _stockService.VerifyStocks(order);
            if (catalogVerified && stockAvailable)
            {
                order.Status = OrderStatus.Validated;
            }
            _orderRepository.Save(order);
        }
    }
}