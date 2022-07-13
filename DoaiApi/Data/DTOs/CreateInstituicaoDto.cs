using System.ComponentModel.DataAnnotations;


namespace DoaiApi.Data.DTOs
{
    public class CreateInstituicaoDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Razão Social é obrigatório")]
        [StringLength(50, ErrorMessage = "Razão Social não pode exceder 50 caracteres")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [StringLength(14, ErrorMessage = "CNPJ não pode exceder 14 caracteres")]
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(2, ErrorMessage = "Estado não pode exceder 2 caracteres")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        [StringLength(25, ErrorMessage = "Cidade não pode exceder 25 caracteres")]
        public string Cidade { get; set; }
    }
}
