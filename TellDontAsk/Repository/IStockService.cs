using System.Threading.Tasks;
using TellDontAsk.Domain;

namespace TellDontAsk.Service
{
    public interface IStockService
    {
        Task<bool> VerifyStocks(Order order);
    }
}