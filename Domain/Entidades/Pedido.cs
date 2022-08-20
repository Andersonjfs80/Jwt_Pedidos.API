using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 2)]
        public bool Status { get; set; } = true;

        [Required]
        [Column(Order = 3)]
        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }
  
        [Required]
        [Column(Order = 4)]
        [Display(Name = "Data de emissão")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataEmissao { get; set; }

        [Column(Order = 5)]
        [Display(Name = "Data de saída")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime? DataSaida { get; set; }

        [Required]
        [Column(Order = 6)]
        [Display(Name = "Data de cadastro")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
