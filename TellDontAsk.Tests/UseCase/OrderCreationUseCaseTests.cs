using System.Collections.Generic;
using TellDontAsk.Domain;
using TellDontAsk.Repository;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
{
    public class OrderCreationUseCaseTests
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly OrderCreationUseCase _useCase;

        public OrderCreationUseCaseTests()
        {
            _orderRepository = new TestOrderRepository();
            var food = new Category()
            {
                Name = "food",
                TaxPercentage = 10
            };
            IProductCatalog productCatalog = new InMemoryProductCatalog(
                new List<Product>
                {
                    new Product()
                    {
                        Name = "salad",
                        Price = 3.56m,
                        Category = food
                    },
                    new Product()
                    {
                        Name = "tomato",
                        Price = 4.65m,
                        Category = food
                    }
                });
            _useCase = new OrderCreationUseCase(_orderRepository, productCatalog);
        }

        [Fact]
        public async void SellMultipleItems()
        {
            var saladRequest = new SellItemRequest()
            {
                ProductName = "salad",
                Quantity = 2
            };

            var tomatoRequest = new SellItemRequest()
            {
                ProductName = "tomato",
                Quantity = 3
            };

            var request = new SellItemsRequest {Requests = new List<SellItemRequest> {saladRequest, tomatoRequest}};

            await _useCase.RunAsync(request);

            var insertedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Created, insertedOrder.Status);
            Assert.Equal(23.20m, insertedOrder.Total);
            Assert.Equal(2.13m, insertedOrder.Tax);
            Assert.Equal("EUR", insertedOrder.Currency);
            Assert.Equal(2, insertedOrder.Items.Count);
            Assert.Equal("salad", insertedOrder.Items[0].Product.Name);
            Assert.Equal(3.56m, insertedOrder.Items[0].Product.Price);
            Assert.Equal(2, insertedOrder.Items[0].Quantity);
            Assert.Equal(7.84m, insertedOrder.Items[0].TaxedAmount);
            Assert.Equal(0.72m, insertedOrder.Items[0].Tax);
            Assert.Equal("tomato", insertedOrder.Items[1].Product.Name);
            Assert.Equal(4.65m, insertedOrder.Items[1].Product.Price);
            Assert.Equal(3, insertedOrder.Items[1].Quantity);
            Assert.Equal(15.36m, insertedOrder.Items[1].TaxedAmount);
            Assert.Equal(1.41m, insertedOrder.Items[1].Tax);
        }

        [Fact]
        public void UnknownProduct()
        {
            var request = new SellItemsRequest {Requests = new List<SellItemRequest>()};
            var unknownProductRequest = new SellItemRequest {ProductName = "unknown product"};
            request.Requests.Add(unknownProductRequest);

            Assert.ThrowsAsync<UnknownProductException>(() => _useCase.RunAsync(request));
        }
    }
}