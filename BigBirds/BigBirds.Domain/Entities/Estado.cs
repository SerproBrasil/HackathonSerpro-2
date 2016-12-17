using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class Estado : EntidadeBase
    {
        #region fields and properties

        public string Nome { get; set; }
        public string Sigla { get; set; }

        #endregion
    }
}
