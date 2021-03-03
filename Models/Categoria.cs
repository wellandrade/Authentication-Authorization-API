﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        public string Titulo { get; set; }
    }
}
