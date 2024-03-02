using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologiesMx.Repository.Repositories
{
    public interface IBaseRepository<TModel> : IDisposable where TModel : class
    {
        IQueryable<TModel> GetAll();
        IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate);
        TModel Find(params object[] id);
        void Add(TModel model);
        void Edit(TModel model);
        void Delete(TModel model);
        void Save();
    }

    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, new()
    {
        protected readonly DbContext _context;
        private IDbContextTransaction _trx;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _trx?.Dispose();
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<TModel> GetAll()
        {
            var query = _context.Set<TModel>();
            return query;
        }

        public IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            var query = GetAll().Where(predicate);
            return query;
        }

        public TModel Find(params object[] id)
        {
            var result = _context.Set<TModel>().Find(id);
            return result;
        }

        public virtual void Add(TModel model)
        {
            _context.Set<TModel>().Add(model);
        }

        public virtual void Edit(TModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public virtual void Delete(TModel model)
        {
            _context.Entry(model).State = EntityState.Deleted;
        }
    }
}
