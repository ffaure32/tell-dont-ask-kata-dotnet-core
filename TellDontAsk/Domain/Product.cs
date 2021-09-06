using System;

namespace TellDontAsk.Domain
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }

        public decimal UnitaryTax => Math.Round(Price / 100 * Category.TaxPercentage, 2, MidpointRounding.AwayFromZero);

        public decimal UnitaryTaxedAmount => Math.Round(Price + UnitaryTax, 2, MidpointRounding.AwayFromZero);
    }
}