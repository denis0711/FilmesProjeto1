using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Business.Model
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
