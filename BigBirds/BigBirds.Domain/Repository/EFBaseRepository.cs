using BigBirds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using BigBirds.Domain.Entities.Seed;
using System.Data.Common;
using BigBirds.Domain.Contexts;

namespace BigBirds.Domain.Repository
{
    public class EFBaseRepository<TEntity> : IUnitOfWork, IRepository<TEntity> where TEntity : EntidadeBase
    {
        #region fields and properties

        bool _disposed;
        private readonly DbContext _dbContext;

        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        #endregion

        #region constructors

        private EFBaseRepository(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName)) throw new ArgumentNullException("connectionName");

            _dbContext = new DbContext(connectionName);

            // setting the db initializer
            // Database.SetInitializer<BigBirdsContext>(new EFBootstrap());
        }
        public EFBaseRepository(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;

            // Database.SetInitializer<BigBirdsContext>(new EFBootstrap());
        }


        ~EFBaseRepository()
        {
            Dispose(false);
        }

        #endregion

        #region interface IRepository<TEntity>


        public void Add(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            DbContext.Set<TEntity>().Add(obj);
        }

        public void Update(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            DbContext.Set<TEntity>().Attach(obj);
            DbContext.Entry(obj).State = EntityState.Modified;
        }

        public ICollection<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> criteria)
        {
            return DbContext.Set<TEntity>().Where(criteria).ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            DbContext.Set<TEntity>().Attach(obj);
            DbContext.Set<TEntity>().Remove(obj);
        }

        public void Remove(int id)
        {
            var entidade = GetById(id);
            DbContext.Set<TEntity>().Attach(entidade);
            DbContext.Set<TEntity>().Remove(entidade);
        }

        #endregion

        #region interface IUnitOfWork

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion
    }
}
