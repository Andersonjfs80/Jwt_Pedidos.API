using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class ProdutoCategoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int ProdutoCategoriaId { get; set; }

        [Required]
        [Column(Order = 1)]
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        [Required]
        [Column(Order = 2)]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Column(Order = 3)]
        [ForeignKey("CategoriaItem")]
        public int CategoriaItemId { get; set; }
        public virtual CategoriaItem CategoriaItem { get; set; }
        public bool Status { get; set; }
    }
}
