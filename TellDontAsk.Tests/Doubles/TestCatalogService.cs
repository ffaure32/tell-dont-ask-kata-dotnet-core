using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Service;
using TellDontAsk.UseCase;

namespace TellDontAsk.Tests.Doubles
{
    public class TestCatalogService : ICatalogService
    {
        private IList<Product> ReferencedProducts { get; } = new List<Product>();
        public async Task<bool> CheckCatalog(Order order)
        {
            var validated = true;
            await Task.Run(() =>
            {
                Thread.Sleep(100);
                if (order.Items.Select(i => i.Product).Any(p => !ReferencedProducts.Contains(p)))
                {
                    validated = false;
                }
            });
            return validated;
        }

        public void AddReferencedProduct(Product referencedProduct)
        {
            ReferencedProducts.Add(referencedProduct);
        }

    }
}