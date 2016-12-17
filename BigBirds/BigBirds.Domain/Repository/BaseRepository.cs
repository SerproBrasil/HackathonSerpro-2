using BigBirds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BigBirds.Domain.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IEntidadeBase
    {
        #region constructors

        public BaseRepository(string connectionName)
        {

        }

        #endregion

        #region interface IRepository<TEntity>


        public int Add(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> GetAll(Expression<Func<TEntity>> criteria)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
