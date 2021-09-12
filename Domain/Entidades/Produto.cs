using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4)]
        [Display(Name = "Nome")]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto reduzido.", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 4)]
        [Display(Name = "Nome reduzido")]
        [Column(Order = 2)]
        public string NomeReduzido { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 3)]
        public bool Status { get; set; }
                
        [Column(Order = 4)]
        [ForeignKey("ProdutoPrecoCusto")]
        public int ProdutoPrecoId { get; set; }
        public virtual ProdutoPreco ProdutoPreco { get; set; }
    }
}
