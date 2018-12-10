using System;
using System.Linq.Expressions;
using ChatBot2000.Core.Data.Interfaces;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;

namespace ChatBot2000.Core.Data
{
    public class ActiveMessagePolicy<T> : ISpecification<T> where T : IDataItem
    {
        public ActiveMessagePolicy()
        {
            Predicate = message => message.DataItemStatus == DataItemStatus.Active;
        }

        public Expression<Func<T, bool>> Predicate { get; }
    }
}
