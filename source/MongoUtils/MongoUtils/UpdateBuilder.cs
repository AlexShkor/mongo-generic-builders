using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoUtils
{
    public abstract class UpdateBuilder<T>
    {
        public abstract UpdateBuilder<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value);
        public abstract UpdateBuilder<T> Push<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value);
        public abstract UpdateBuilder<T> Pull<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value);
        public abstract UpdateBuilder<T> AddToSet<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value);
    }
}
