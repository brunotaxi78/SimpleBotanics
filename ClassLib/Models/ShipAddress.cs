using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassLib.Models
{
    public class ShipAddress
    {
        public int ShipAddressId { get; set; }

        public int OrderId { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher a Morada")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Mínimo 10 caracteres")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher a Cidade")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Mínimo 3 caracteres")]
        public string State { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher o Código Postal")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Preenchido incorretamente : XXXX-XXX")]
        [StringLength(8)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher a Cidade")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Mínimo 3 caracteres")]
        public string Country { get; set; }

        public string Name { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
