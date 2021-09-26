using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id#")]
        [Column(Order = 0)]
        public int UsuarioId { get; set; }

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

        //[Required(ErrorMessage = "Informe uma chave secreta.", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Chave secreta")]
        [Column(Order = 2)]
        public string SecretKey { get; set; }
    }
}
