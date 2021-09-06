using System;

namespace TellDontAsk.Domain
{
    public class OrderItem
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            
        }
        public Product Product { get; }
        public int Quantity { get; }

        public decimal TaxedAmount =>
            Math.Round(Product.UnitaryTaxedAmount * Quantity, 2, MidpointRounding.AwayFromZero);

        public decimal Tax => Product.UnitaryTax * Quantity;

    }
}