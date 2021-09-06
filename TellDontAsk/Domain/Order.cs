using System.Collections.Generic;
using System.Linq;

namespace TellDontAsk.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public OrderStatus Status { get; set; }
        public IList<OrderItem> Items { get; set; }
        public decimal Tax => Items.Sum(i => i.Tax);
        public decimal Total => Items.Sum(i => i.TaxedAmount);

        private Order()
        {
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
            Currency = "EUR";
        }

        public static Order Create()
        {
            return new Order();
        }
        
        public void AddItem(Product product, int quantity)
        {
            var orderItem = new OrderItem(product, quantity);
            Items.Add(orderItem);
        }
    }
}