using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Service;
using TellDontAsk.UseCase;

namespace TellDontAsk.Tests.Doubles
{
    public class TestStockService : IStockService
    {
        private IDictionary<Product, int> StockByProduct { get; } = new Dictionary<Product, int>();

        public async Task<bool> VerifyStocks(Order order)
        {
            var validated = true;
            await Task.Run(() =>
            {
                Thread.Sleep(Constants.Latency);
                if (order.Items.Select(i => (i.Product, i.Quantity)).Any(s =>
                    !StockByProduct.ContainsKey(s.Product) || StockByProduct[s.Product] < s.Quantity))
                {
                    validated = false;
                }
            });
            return validated;
        }

        public void AddStock(Product product, int quantity)
        {
            StockByProduct[product] = quantity;
        }
    }
}