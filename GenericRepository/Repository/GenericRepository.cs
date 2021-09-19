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
        /// <summary>
        /// get all from entity
        /// </summary>
        /// <param name="take">number to take from entity</param>
        /// <param name="skip">number to skip from entity</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(int take, int skip=0)
        {
            return await entity.Take(take).Skip(skip).ToListAsync();
        }

        /// <summary>
        /// get all from entity with expression
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <param name="take">number to take from entity</param>
        /// <param name="skip">number to skip from entity</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, int take, int skip=0)
        {
            return await entity.Where(where).Take(take).Skip(skip).ToListAsync();
        }

        /// <summary>
        /// skip all until expression is true
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <param name="take">number to take from entity</param>
        /// <param name="skip">number to skip from entity</param>
        /// <returns></returns>

        public IEnumerable<T> GetAllWithSkipWhile(Expression<Func<T, bool>> where, int take, int skip = 0)
        {
            return entity.SkipWhile(where).Take(take).Skip(skip);
        }

        /// <summary>
        /// take all until expression is true
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <param name="take">number to take from entity</param>
        /// <param name="skip">number to skip from entity</param>
        /// <returns></returns>
        public IEnumerable<T> GetAllWithTakeWhile(Expression<Func<T, bool>> where, int take, int skip = 0)
        {
            return entity.TakeWhile(where).Take(take).Skip(skip);
        }

        /// <summary>
        /// get number of all in entity
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetAllCountAsync()
        {
            return await entity.CountAsync();
        }

        /// <summary>
        /// get number of all in entity with expression
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <returns></returns>
        public async Task<int> GetAllCountAsync(Expression<Func<T, bool>> where)
        {
            return await entity.CountAsync(where);
        }

        /// <summary>
        /// get single entity with a unique key
        /// </summary>
        /// <param name="id">unique key of entity</param>
        /// <returns></returns>
        public async Task<T> GetAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        /// <summary>
        /// get single entity with expression
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await entity.FirstOrDefaultAsync(where);
        }

        /// <summary>
        /// get last entity
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetLastAsync()
        {
            return await entity.LastOrDefaultAsync();
        }
        #endregion

        #region Command
        /// <summary>
        /// insert new entity
        /// </summary>
        /// <param name="model">entity model</param>
        /// <returns></returns>
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

        /// <summary>
        /// insert new entities
        /// </summary>
        /// <param name="models">entities models</param>
        /// <returns></returns>
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

        /// <summary>
        /// get average number of entity with expression
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <returns></returns>
        public async Task<decimal> GetAverageAsync(Expression<Func<T, decimal>> where)
        {
            return await entity.AverageAsync(where);
        }

        /// <summary>
        /// group by entity with property name of entity
        /// </summary>
        /// <param name="propertyName">property name of entity to groupby</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<object, T>> GetGroupBy(string propertyName)
        {
            return entity.Cast<T>().GroupBy(g=>g.GetProperty(propertyName));
        }

        /// <summary>
        /// get maximum number of entity with property name
        /// </summary>
        /// <param name="propertyName">property name of entity to calculate maximum</param>
        /// <returns></returns>
        public async Task<object> GetMaxAsync(string propertyName)
        {
            return await entity.MaxAsync(o => o.GetProperty(propertyName));
        }

        /// <summary>
        /// get minimum number of entity with property name
        /// </summary>
        /// <param name="propertyName">property name of entity to calculate minimum</param>
        /// <returns></returns>
        public async Task<object> GetMinAsync(string propertyName)
        {
            return await entity.MinAsync(o => o.GetProperty(propertyName));
        }

        /// <summary>
        /// get sum number of entity with expression
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <returns></returns>
        public async Task<decimal> GetSumAsync(Expression<Func<T, decimal>> where)
        {
            return await entity.SumAsync(where);
        }

        /// <summary>
        /// get sum 
        /// </summary>
        /// <param name="where">expression to filter entity</param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> where)
        {
            return await entity.AnyAsync(where);
        }

        /// <summary>
        /// update entity with model
        /// </summary>
        /// <param name="model">entity model</param>
        /// <returns></returns>
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

        /// <summary>
        /// update entities with models
        /// </summary>
        /// <param name="models">entity models</param>
        /// <returns></returns>
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

        /// <summary>
        /// remove entity with model
        /// </summary>
        /// <param name="model">entity model</param>
        /// <returns></returns>
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

        /// <summary>
        /// remove entities with models
        /// </summary>
        /// <param name="models">entity models</param>
        /// <returns></returns>
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
        /// <summary>
        /// save changes async
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
        /// <summary>
        /// save changes
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
        #endregion
    }
}
