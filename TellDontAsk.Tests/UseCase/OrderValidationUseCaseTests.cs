using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
{
    public class OrderValidationUseCaseTests
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly OrderValidationUseCase _useCase;
        private readonly TestCatalogService _catalogService;
        private readonly TestStockService _stockService;
        public OrderValidationUseCaseTests()
        {
            _orderRepository = new TestOrderRepository();
            _catalogService = new TestCatalogService();
            _stockService = new TestStockService();
            _useCase = new OrderValidationUseCase(_orderRepository, _catalogService, _stockService);
        }
        
        [Fact]
        public async Task CannotValidateUnreferencedProduct()
        {
            var unreferencedProduct = new Product{
                Category = new Category(),
                Price = 12m,
                Name = "produit pourri"
            };
            var orderItem = new OrderItem() { 
                Product = unreferencedProduct,
                Quantity = 12
            };
            var initialOrder = new Order {
                Status = OrderStatus.Approved, 
                Id = 1, 
                Items = new List<OrderItem> { orderItem }
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderValidationRequest {OrderId = 1};

            await _useCase.Run(request);
            
            var validated = _orderRepository.GetSavedOrder();
            Assert.NotEqual(OrderStatus.Validated, validated.Status);

        }
        
        [Fact]
        public async Task CannotValidateOutOfStockProduct()
        {
            var referencedProduct = new Product{
                Category = new Category(),
                Price = 12m,
                Name = "produit connu"
            };
            _catalogService.AddReferencedProduct(referencedProduct);
            var orderItem = new OrderItem() { 
                Product = referencedProduct,
                Quantity = 12
            };
            var initialOrder = new Order {
                Status = OrderStatus.Approved, 
                Id = 1, 
                Items = new List<OrderItem> { orderItem }
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderValidationRequest {OrderId = 1};

            await _useCase.Run(request);
            
            var validated = _orderRepository.GetSavedOrder();
            Assert.NotEqual(OrderStatus.Validated, validated.Status);
        }

        [Fact]
        public async Task ReferencedProductInStockCanBeValidated()
        {
            var referencedProduct = new Product{
                Category = new Category(),
                Price = 12m,
                Name = "produit connu"
            };
            _catalogService.AddReferencedProduct(referencedProduct);
            var quantity = 12;
            _stockService.AddStock(referencedProduct, quantity+1);
            var orderItem = new OrderItem() { 
                Product = referencedProduct,
                Quantity = quantity,
            };
            var initialOrder = new Order {
                Status = OrderStatus.Approved, 
                Id = 1, 
                Items = new List<OrderItem> { orderItem }
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderValidationRequest {OrderId = 1};

            await _useCase.Run(request);
            
            var validated = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Validated, validated.Status);

        }

    }
}