using System.Collections.Generic;


namespace ClassLib.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int SubCategory { get; set; }

        public ICollection<Product> Products { get; set; }


    }
}