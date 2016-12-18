using BigBirds.Domain.Contexts;
using BigBirds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Repository
{
    public class ConclusaoReclamacaoRepository : EFBaseRepository<ConclusaoReclamacao>, IConclusaoReclamacaoRepository
    {
        public ConclusaoReclamacaoRepository() : base(new BigBirdsContext())
        {

        }
        public ConclusaoReclamacaoRepository(BigBirdsContext context) : base(context)
        {

        }

        public ICollection<ConclusaoReclamacao> GetConclusoesByKeyWords(string keywords)
        {
            return DbContext.Set<ConclusaoReclamacao>().ToList().Where(x => x.PalavrasChaves == keywords).ToList();
        }
    }

    public interface IConclusaoReclamacaoRepository
    {
        ICollection<ConclusaoReclamacao> GetConclusoesByKeyWords(string keywords);
    }
}
