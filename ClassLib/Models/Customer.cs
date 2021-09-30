using ClassLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClassLib.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public double ValorBonus { get; set; }

        public int NumBonus { get; set; }

        public bool Admin { get; set; }

        public ICollection<BillingAddress> BillingAddresses { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}