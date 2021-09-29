using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Models
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Informe o nome do usuário.", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4)]
        [Display(Name = "Nome")]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe uma senha.", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Senha")]
        [Column(Order = 1)]
        public string Senha { get; set; }
    }
}
