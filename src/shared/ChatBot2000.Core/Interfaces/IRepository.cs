using System.Collections.Generic;
using ChatBot2000.Core.Data;
using ChatBot2000.Core.Data.Interfaces;

namespace ChatBot2000.Core.Interfaces
{
    public interface IRepository
    {
        List<T> List<T>(ISpecification<T> spec) where T : DataItem;
        T Create<T>(T dataItem) where T : DataItem;
        void Update<T>(T dataItem) where T : DataItem;
        List<T> Create<T>(List<T> dataItemList) where T : DataItem;
    }
}
