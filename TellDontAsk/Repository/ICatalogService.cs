using System.Threading.Tasks;
using TellDontAsk.Domain;

namespace TellDontAsk.Service
{
    public interface ICatalogService
    {
        Task<bool> CheckCatalog(Order order);
    }
}