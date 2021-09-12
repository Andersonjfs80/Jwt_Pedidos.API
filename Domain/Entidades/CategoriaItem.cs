using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades
{
    public class CategoriaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int CategoriaItemId { get; set; }

        [Required(ErrorMessage = "Informe o nome da subcategoria.", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 4)]
        [Display(Name = "Nome")]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 2)]
        public bool Status { get; set; }

        [Column(Order = 3)]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
