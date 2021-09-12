using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class Unidade
    {
        public int UnidadeId { get; set; }

        [Required(ErrorMessage = "Informe o nome da unidade.", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 2)]
        [Display(Name = "Nome")]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a sigla da unidade.", AllowEmptyStrings = false)]
        [StringLength(6, MinimumLength = 2)]
        [Display(Name = "Sigla")]
        [Column(Order = 2)]
        public string NomeReduzido { get; set; }

        [Required(ErrorMessage = "Informe o status.")]
        [Display(Name = "Status")]
        [Column(Order = 4)]
        public bool Status { get; set; }
    }
}
