using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class TabelaPreco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int TabelaPrecoId { get; set; }

        [Required(ErrorMessage = "Informe o nome.", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 4)]
        [Display(Name = "Nome")]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 2)]
        public bool Status { get; set; }
    }
}
