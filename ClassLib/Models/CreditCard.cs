using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassLib.Models
{
   public class CreditCard
    {
        public int CreditCardId { get; set; }
        [Required]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher Nome do Cartão")]
        [StringLength(80, MinimumLength = 3)]
        public string CardName { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher Número do Cartão")]
        public int CardNumber { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher Número de Segurança")]
        public string SecurityNumber { get; set; }
        [Required(ErrorMessage = "Obrigatório preencher a Data de Validade")]
        [RegularExpression(@"(?:0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Preenchido incorretamente : MM/AA")]
        public string ExpDate { get; set; } 

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
