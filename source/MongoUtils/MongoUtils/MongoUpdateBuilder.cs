using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace MongoUtils
{
    public class MongoUpdateBuilder<T> : UpdateBuilder<T> where T : class
    {
        public MongoDB.Driver.Builders.UpdateBuilder ResultQuery
        {
            get { return query; }
        }

        private MongoDB.Driver.Builders.UpdateBuilder query = new MongoDB.Driver.Builders.UpdateBuilder();

        public override UpdateBuilder<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            var selectQuery = BuildSelectQuery(expression);
            query = query.Set(selectQuery, BuildBsonValue(value));
            return this;
        }

        private static string BuildSelectQuery<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var body = expression.Body.ToString().Replace("get_Item(", "").Replace("IsAny()", "$").Replace(')', '.');
            var parts = body.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                return parts[1];
            }
            var list = new List<string>(parts);
            list.RemoveAt(0);
            return string.Join(".", list);
        }

        public override UpdateBuilder<T> Push<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            var selectQuery = BuildSelectQuery(expression);
            query = query.Push(selectQuery, BuildBsonValue(value));
            return this;
        }

        public override UpdateBuilder<T> Pull<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            var selectQuery = BuildSelectQuery(expression);
            query = query.Pull(selectQuery, BuildBsonValue(value));
            return this;
        }

        public override UpdateBuilder<T> AddToSet<TProperty>(Expression<Func<T, List<TProperty>>> expression, TProperty value)
        {
            var selectQuery = BuildSelectQuery(expression);
            query = query.AddToSet(selectQuery, BuildBsonValue(value));
            return this;
        }

        private static BsonValue BuildBsonValue<TProperty>(TProperty value)
        {
            BsonValue bsonValue;
            return BsonTypeMapper.TryMapToBsonValue(value, out bsonValue) ? bsonValue : value.ToBsonDocument();
        }
    }
}