using System;
using System.Linq.Expressions;

namespace ChatBot2000.Core.Data.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
