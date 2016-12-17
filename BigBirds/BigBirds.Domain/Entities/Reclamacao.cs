using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class Reclamacao : EntidadeBase
    {
        #region fields and properties

        public string Protocolo { get; set; }
        public Orgao OrgaoCompetente { get; set; }
        public DateTime CriadoEm { get; set; }
        public SituacaoReclamacao Situacao { get; set; }

        #endregion
    }
}
