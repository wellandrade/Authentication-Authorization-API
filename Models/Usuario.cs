using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Esse campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter entre 3 e 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Esse campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter entre 3 e 20 caracteres")]
        public string Senha { get; set; }
        public string Perfil { get; set; }
    }
}
