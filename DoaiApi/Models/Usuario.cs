using System.ComponentModel.DataAnnotations;

namespace DoaiApi.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }

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
