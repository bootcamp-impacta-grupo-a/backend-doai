using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoaiApi.Data.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(300, ErrorMessage = "Limite de 300 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [StringLength(250)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [StringLength(300, ErrorMessage = "Limite de 300 caracteres")]
        public string Login { get; set; }
    }
}
