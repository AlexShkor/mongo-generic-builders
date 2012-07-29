using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoUtils
{
    public static class MongoUpdate<T> where T: class 
    {
        public static UpdateBuilder<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            return Create().Set(expression, value);
        }
        public static UpdateBuilder<T> Push<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            return Create().Push(expression, value);
        }
        public static UpdateBuilder<T> Pull<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            return Create().Pull(expression, value);
        }
        public static UpdateBuilder<T> AddToSet<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            return Create().AddToSet(expression, value);
        }

        private static UpdateBuilder<T> Create()
        {
            return new MongoUpdateBuilder<T>();
        }
    }
}