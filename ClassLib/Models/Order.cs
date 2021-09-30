using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLib.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime ShipDate { get; set; }
        public double Freight { get; set; }
        public double SalesTax { get; set; }

        public double Discount { get; set; }

        public string Info { get; set; }
        public double Paid { get; set; }
        public DateTime PaymentDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("BillingAddress")]
        public int? BillingAddressId { get; set; }
        public BillingAddress BillingAddress { get; set; }

        [ForeignKey("ShipAddress")]
        public int? ShipAddressId { get; set; }
        public ShipAddress ShipAddress { get; set; }

        [ForeignKey("Status")]
        public int? StatusId { get; set; }
        public Status Status { get; set; }

    }
}