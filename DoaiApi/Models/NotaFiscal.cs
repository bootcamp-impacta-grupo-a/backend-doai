using System;
using System.ComponentModel.DataAnnotations;


namespace DoaiApi.Models
{
    public class NotaFiscal
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FileName { get; set; }

        [Required]
        [StringLength(150)]
        public string ContentType { get; set; }

        [Required]
        public byte[] ArrayBytes { get; set; }
    }
}
