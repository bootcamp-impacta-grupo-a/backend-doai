using System.ComponentModel.DataAnnotations;

namespace DoaiApi.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Limite de 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [StringLength(30, ErrorMessage = "Limite de 30 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [StringLength(200, ErrorMessage = "Limite de 200 caracteres")]
        public string Login { get; set; }

    }
}
