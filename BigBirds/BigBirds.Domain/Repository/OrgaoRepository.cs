using BigBirds.Domain.Contexts;
using BigBirds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Repository
{
    public class OrgaoRepository : EFBaseRepository<Orgao>, IOrgaoRepository
    {
        #region constructors

        public OrgaoRepository() : base(new BigBirdsContext())
        {

        }
        public OrgaoRepository(BigBirdsContext context) : base(context)
        {

        }

        #endregion

        #region IOrgaoRepository interface

        public ICollection<Orgao> GetOrgaosByCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria)) throw new ArgumentNullException("categoria");

            return DbContext.Set<Orgao>().Where(o => o.Categoria.Nome.ToLower() == categoria.ToLower()).ToList();
        }

        #endregion
    }

    public interface IOrgaoRepository
    {
        ICollection<Orgao> GetOrgaosByCategoria(string categoria);
    }
}
