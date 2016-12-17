using BigBirds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntidadeBase
    {
        TEntity GetById(int id);
        ICollection<TEntity> GetAll();
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> criteria);
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Remove(int id);
    }
}
