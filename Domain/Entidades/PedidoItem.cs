using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class PedidoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int PedidoItemId { get; set; }

        [Column(Order = 1)]
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }

        [Column(Order = 3)]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        [Column(Order = 4)]
        public int TabelaPrecoId { get; set; }
        public virtual TabelaPreco TabelaPreco { get; set; }

        [Column(Order =  5)]
        public int UnidadeId { get; set; }
        public virtual Unidade Unidade { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4)]
        [Display(Name = "Nome")]
        [Column(Order = 6)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 7)]
        public bool Status { get; set; } = true;

        [Required]
        [Column(Order = 8)]
        [Display(Name = "Valor unitário")]
        public double ValorUnitario { get; set; }

        [Required]
        [Column(Order = 9)]
        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Required]
        [Column(Order = 10)]
        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        [Required]
        [Column(Order = 11)]
        [Display(Name = "Data de cadastro")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
