using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassLib.Models
{
    public class BillingAddress
    {
        public int BillingAddressId { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Order> Orders { get; set; }
        
    }
}