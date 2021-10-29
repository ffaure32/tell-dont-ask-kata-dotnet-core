using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class InMemoryProductCatalog : IProductCatalog
    {
        private readonly IList<Product> _products;

        public InMemoryProductCatalog(IList<Product> products)
        {
            this._products = products;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            await Task.Delay(100);
            return _products.FirstOrDefault(p => p.Name == name);
        }
    }
}