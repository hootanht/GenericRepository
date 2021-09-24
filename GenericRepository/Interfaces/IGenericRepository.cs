using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region Query
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(int take,int skip=0);
        Task<IEnumerable<T>> FilterAsync(string query,string propertyName);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, int take, int skip=0);
        IEnumerable<T> GetAllWithSkipWhile(Expression<Func<T, bool>> where, int take, int skip = 0);
        IEnumerable<T> GetAllWithTakeWhile(Expression<Func<T, bool>> where, int take, int skip = 0);
        Task<int> GetAllCountAsync();
        Task<int> GetAllCountAsync(Expression<Func<T, bool>> where);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<T> GetLastAsync();
        Task<decimal> GetAverageAsync(Expression<Func<T, decimal>> where);
        IEnumerable<IGrouping<object, T>> GetGroupBy(string propertyName);
        Task<object> GetMaxAsync(string propertyName);
        Task<object> GetMinAsync(string propertyName);
        Task<decimal> GetSumAsync(Expression<Func<T, decimal>> where);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> where);
        #endregion

        #region Command
        Task<bool> InsertAsync(T model);
        Task<bool> InsertAsync(IEnumerable<T> models);
        bool Update(T model);
        bool Update(IEnumerable<T> models);
        bool Remove(T model);
        bool Remove(IEnumerable<T> models);
        #endregion

        #region Save
        Task SaveAsync();
        void Save();
        #endregion
    }
}
