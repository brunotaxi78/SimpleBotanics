using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassLib.Models
{
   public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
