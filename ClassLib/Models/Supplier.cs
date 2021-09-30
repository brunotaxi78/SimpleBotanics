using System.Collections.Generic;


namespace ClassLib.Models
{

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string DiscountType { get; set; }
        public string Notes { get; set; }

        public ICollection<ProductSupplier> ProductsSuppliers { get; set; }
    }
}

