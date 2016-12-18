using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class ConclusaoReclamacao : EntidadeBase
    {
        public string Conclusao { get; set; }
        public string PalavrasChaves { get; set; }
    }
}
