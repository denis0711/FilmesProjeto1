using System.Collections.Generic;

namespace Site.Business.Model
{
    public class Distribuidora : Entity
    {
        public string Nome { get; set; }
    
        public string Sobre { get; set; }

        public string Imagem { get; set; }

        public IEnumerable<Filme> Filmes { get; set; }
    }
}
