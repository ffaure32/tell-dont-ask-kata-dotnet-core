using TellDontAsk.Domain;

namespace TellDontAsk.Repository
{
    public interface IProductCatalog
    {
        Product GetByName(string name);
    }
}