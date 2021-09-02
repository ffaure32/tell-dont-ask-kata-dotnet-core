using System.Collections.Generic;
using System.Linq;
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

        public Product GetByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }
    }
}