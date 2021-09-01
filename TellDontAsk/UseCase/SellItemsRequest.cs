using System.Collections.Generic;

namespace TellDontAsk.UseCase
{
    public class SellItemsRequest
    {
        public IList<SellItemRequest> Requests { get; set; }
    }
}