using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChatBot2000.Core.Data;
using ChatBot2000.Core.Data.Interfaces;
using ChatBot2000.Core.Interfaces;
using Newtonsoft.Json;

namespace ChatBot2000.Infrastructure.Json
{
    public class GenericJsonFileRepository : IRepository
    {
        public T Single<T>(ISpecification<T> spec) where T : DataItem
        {
            var list = GetList<T>();
            return list.FirstOrDefault(i => spec.Predicate.Compile().Invoke(i));
        }

        public List<T> List<T>(ISpecification<T> spec) where T : DataItem
        {
            var list = GetList<T>();
            return list.Where(i => spec.Predicate.Compile().Invoke(i)).ToList();
        }

        public T Create<T>(T dataItem) where T : DataItem
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T dataItem) where T : DataItem
        {
            throw new NotImplementedException();
        }

        public List<T> Create<T>(List<T> dataItemList) where T : DataItem
        {
            throw new NotImplementedException();
        }

        private static List<T> GetList<T>()
        {
            var list = new List<T>();

            var filePath = $"FileDataStore\\{typeof(T).Name}.json";

            try
            {
                var fullText = File.ReadAllText(filePath);
                list = JsonConvert.DeserializeObject<List<T>>(fullText, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return list;
        }
    }
}
