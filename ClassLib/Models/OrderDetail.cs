using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLib.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public string User { set; get; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Tax { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { set; get; }
        public double Total { get; set; }

       
        [ForeignKey("Order")]
        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
