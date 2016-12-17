using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class Localizacao : EntidadeBase
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Descricao { get; set; }
    }
}
