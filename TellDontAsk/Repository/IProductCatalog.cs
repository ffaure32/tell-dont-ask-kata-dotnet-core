using System.Threading.Tasks;
using TellDontAsk.Domain;

namespace TellDontAsk.Repository
{
    public interface IProductCatalog
    {
        Task<Product> GetByNameAsync(string name);
    }
}