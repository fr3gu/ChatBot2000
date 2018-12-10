using System;
using System.Linq.Expressions;
using ChatBot2000.Core.Data.Interfaces;
using ChatBot2000.Core.Messaging;

namespace ChatBot2000.Core.Data
{
    public class DataItemPolicy<T> : ISpecification<T> where T : DataItem
    {
        public DataItemPolicy(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public static DataItemPolicy<T> ByStatus(DataItemStatus dataItemStatus)
        {
            return new DataItemPolicy<T>(t => t.DataItemStatus == dataItemStatus);
        }

        public static DataItemPolicy<T> ActiveOnly()
        {
            return ByStatus(DataItemStatus.Active);
        }

        public Expression<Func<T, bool>> Predicate { get; }
    }
}