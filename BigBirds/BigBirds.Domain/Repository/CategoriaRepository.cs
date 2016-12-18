using BigBirds.Domain.Contexts;
using BigBirds.Domain.Entities;

namespace BigBirds.Domain.Repository
{
    public class CategoriaRepository : EFBaseRepository<Categoria>
    {
        #region constructors

        public CategoriaRepository() : base(new BigBirdsContext())
        {

        }
        public CategoriaRepository(BigBirdsContext context) : base(context)
        {

        }

        #endregion
    }
}
