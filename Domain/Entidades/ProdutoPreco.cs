using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class ProdutoPreco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int ProdutoPrecoId { get; set; }

        [Required(ErrorMessage = "Informe o preço de custo.")]
        [Display(Name = "Preço de custo")]
        [Column(Order = 1)]
        public double PrecoCusto { get; set; }

        [Required(ErrorMessage = "Informe o preço de custo.")]
        [Display(Name = "Preço de venda")]
        [Column(Order = 2)]
        public double PrecoVenda { get; set; }

        [Required(ErrorMessage = "Informe a quantidade.")]
        [Display(Name = "Quantidade")]
        [Column(Order = 3)]
        [Range(1, 9999999999999999.99)]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 3)]
        public bool Status { get; set; } = true;

        [Required]
        [Column(Order = 4)]
        [ForeignKey("TabelaPreco")]
        public int TabelaPrecoId { get; set; }
        public virtual TabelaPreco TabelaPreco { get; set; }

        [Required]
        [Column(Order = 5)]
        [ForeignKey("Unidade")]
        public int UnidadeId { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
