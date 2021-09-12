using GenericRepository.Utilities;

using GenericRepository.Context;
using GenericRepository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Private Fields
        private readonly GenericRepositoryContext _db;
        private DbSet<T> entity;
        #endregion

        #region Constructor
        public GenericRepository(GenericRepositoryContext db)
        {
            _db = db;
            entity = _db.Set<T>();
        }
        #endregion

        #region Query
        public async Task<IEnumerable<T>> GetAllAsync(int take, int skip=0)
        {
            return await entity.Take(take).Skip(skip).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, int take, int skip=0)
        {
            return await entity.Where(where).Take(take).Skip(skip).ToListAsync();
        }

        public IEnumerable<T> GetAllWithSkipWhile(Expression<Func<T, bool>> where, int take, int skip = 0)
        {
            return entity.SkipWhile(where).Take(take).Skip(skip);
        }

        public IEnumerable<T> GetAllWithTakeWhile(Expression<Func<T, bool>> where, int take, int skip = 0)
        {
            return entity.TakeWhile(where).Take(take).Skip(skip);
        }

        public async Task<int> GetAllCountAsync()
        {
            return await entity.CountAsync();
        }

        public async Task<int> GetAllCountAsync(Expression<Func<T, bool>> where)
        {
            return await entity.CountAsync(where);
        }

        public async Task<T> GetAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await entity.FirstOrDefaultAsync(where);
        }

        public async Task<T> GetLastAsync()
        {
            return await entity.LastOrDefaultAsync();
        }
        #endregion

        #region Command
        public async Task<bool> InsertAsync(T model)
        {
            try
            {
                await entity.AddAsync(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertAsync(IEnumerable<T> models)
        {
            try
            {
                await entity.AddRangeAsync(models);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<decimal> GetAverageAsync(Expression<Func<T, decimal>> where)
        {
            return await entity.AverageAsync(where);
        }

        public IEnumerable<IGrouping<object, T>> GetGroupBy(string propertyName)
        {
            return entity.Cast<T>().GroupBy(g=>g.GetProperty(propertyName));
        }

        public async Task<object> GetMaxAsync(string propertyName)
        {
            return await entity.MaxAsync(o => o.GetProperty(propertyName));
        }

        public async Task<object> GetMinAsync(string propertyName)
        {
            return await entity.MinAsync(o => o.GetProperty(propertyName));
        }

        public async Task<decimal> GetSumAsync(Expression<Func<T, decimal>> where)
        {
            return await entity.SumAsync(where);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> where)
        {
            return await entity.AnyAsync(where);
        }

        public async Task<bool> IsAllAsync(Expression<Func<T, bool>> where)
        {
            return await entity.AllAsync(where);
        }

        public bool Update(T model)
        {
            try
            {
                entity.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(IEnumerable<T> models)
        {
            try
            {
                entity.UpdateRange(models);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(T model)
        {
            try
            {
                entity.Remove(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(IEnumerable<T> models)
        {
            try
            {
                entity.RemoveRange(models);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        #endregion

        #region Save
        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        #endregion
    }
}
