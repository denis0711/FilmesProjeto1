using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.App.Models
{
    public class DistribuidoraViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O {0} e Obrigatorio !!")]
        [StringLength(100, ErrorMessage = "O {0} Tem que que ter de {1} ate {2} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O {0} e Obrigatorio !!")]
        [StringLength(2000, ErrorMessage = "O {0} Tem que que ter de {1} ate {2} caracteres", MinimumLength = 3)]
        public string Sobre { get; set; }
        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }


        [NotMapped]
        public IEnumerable<FilmeViewModel> Filmes { get; set; }
    }
}
