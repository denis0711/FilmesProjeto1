using System;

namespace Site.Business.Model
{
    public class Filme : Entity
    {
        public Guid DistribuidoraId { get; set; }
        public string Nome { get; set; }

        public string Sinopse { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }

        public Distribuidora Distribuidora { get; set; }
    }
}
