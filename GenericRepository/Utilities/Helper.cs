using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Utilities
{
    public static class Helper
    {
        public static Expression<Func<T, object>> GroupByExpression<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var body = Expression.Property(parameter, propertyName);
            return Expression.Lambda<Func<T, object>>(Expression.Convert(body, typeof(object)), parameter);
        }

        public static object GetProperty(this object source, string name)
        {
            if (source == null) return null;
            var pi = source.GetType().GetProperty(name);
            if (pi == null) return null;
            return pi.GetValue(source);
        }
    }
}
