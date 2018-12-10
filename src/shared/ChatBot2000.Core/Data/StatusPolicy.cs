using System;
using System.Linq.Expressions;
using ChatBot2000.Core.Data.Interfaces;
using ChatBot2000.Core.Messaging;

namespace ChatBot2000.Core.Data
{
    public class StatusPolicy<T> : ISpecification<T> where T : DataItem
    {
        public StatusPolicy(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public static StatusPolicy<T> ByStatus(DataItemStatus dataItemStatus)
        {
            return new StatusPolicy<T>(t => t.DataItemStatus == dataItemStatus);
        }

        public static StatusPolicy<T> ActiveOnly()
        {
            return ByStatus(DataItemStatus.Active);
        }

        public Expression<Func<T, bool>> Predicate { get; }
    }
}
