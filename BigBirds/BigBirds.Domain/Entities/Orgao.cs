using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class Orgao : EntidadeBase
    {
        #region fields and properties

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Uri UrlPagina { get; set; }

        #endregion
    }
}
