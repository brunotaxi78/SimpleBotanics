using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClassLib.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int SKU { get; set; }
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public double Size { get; set; }
        public double Stock { get; set; }
        public string Color { get; set; }
        public string Picture { get; set; }
        public double Ranking { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductSupplier> ProductsSuppliers { get; set; }

    }

}
