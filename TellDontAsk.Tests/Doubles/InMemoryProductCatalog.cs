using System.Collections.Generic;
using System.Linq;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class InMemoryProductCatalog : IProductCatalog
    {
        private readonly IList<Product> products;

        public InMemoryProductCatalog(IList<Product> products)
        {
            this.products = products;
        }

        public Product GetByName(string name)
        {
            return products.FirstOrDefault(p => p.Name == name);
        }
    }
}