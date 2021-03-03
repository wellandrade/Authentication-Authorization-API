using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        public string Titulo { get; set; }

        [MaxLength(1024, ErrorMessage = "Esse campo deve conter no maximo 1024 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, 10000, ErrorMessage = "O preço deve ser maior do que zero e menor que 10 mil reais")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior do que zero")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

    }
}
