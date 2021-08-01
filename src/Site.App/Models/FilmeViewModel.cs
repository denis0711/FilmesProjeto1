using Microsoft.AspNetCore.Http;
using Site.App.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Site.App.Models
{
    public class FilmeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [DisplayName("Fornecedor")]
        public Guid DistribuidoraId { get; set; }

        [Required(ErrorMessage ="O {0} e Obrigatorio !!")]
        [StringLength(100, ErrorMessage ="O {0} Tem que que ter de {1} ate {2} caracteres", MinimumLength =3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} e Obrigatorio !!")]
        [StringLength(2000, ErrorMessage = "O {0} Tem que que ter de {1} ate {2} caracteres", MinimumLength = 3)]
        public string Sinopse { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Produto")]

        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [Moeda]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [DisplayName("Genero")]
        public string Categoria { get; set; }

        [NotMapped]
        public DistribuidoraViewModel Distribuidora { get; set; }
        [NotMapped]
        public IEnumerable<DistribuidoraViewModel> Distribuidoras { get; set; }

  
    }
}
