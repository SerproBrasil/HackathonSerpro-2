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

        public virtual Estado Estado { get; set; }
        public virtual Categoria Categoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UrlPagina { get; set; }

        #endregion
    }
}
