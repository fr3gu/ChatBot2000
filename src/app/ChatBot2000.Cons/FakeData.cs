using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Core.Messaging.Interfaces;
using Newtonsoft.Json;

namespace ChatBot2000.Cons
{
    public class FakeData
    {
        private static long GetMillisecondsFromMinutes(int i)
        {
            return i * 60 * 1000;
        }

        private static List<IAutoMessage> GetRepeatingMessages()
        {
            var automatedMessages = new List<IAutoMessage> {
                new RepeatingMessage(3000, "Hello and welcome! I hope you're enjoying the stream! Feel free to follow along, make suggestions, ask questions, or contribute! And make sure you click the follow button to know when the next stream is!", DataItemStatus.Active),
                new RepeatingMessage(2000, "foo", DataItemStatus.Draft),
                new RepeatingMessage(1000, "bar", DataItemStatus.Disabled),
            };
            return automatedMessages;
        }

        private static List<ICommandMessage> GetICommandMessages()
        {
            return new List<ICommandMessage>
            {
                new StaticCommandResponseMessage("coins", "Coins?!?! I think you meant !points", DataItemStatus.Active),
            };

        }

        public static void Initialize()
        {
            StoreData(GetRepeatingMessages());

            StoreData(GetICommandMessages());
        }

        private static void StoreData<T>(List<T> automatedMessages)
        {
            var serializedJson = JsonConvert.SerializeObject(automatedMessages, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
            });

            WriteDateToFileSystem(typeof(T).Name, serializedJson);
        }

        private static void WriteDateToFileSystem(string dataTypeName, string textToWrite)
        {
            var filePath = $"FileDataStore\\{dataTypeName}.json";
            if (!Directory.Exists("FileDataStore"))
            {
                Directory.CreateDirectory("FileDataStore");
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            try
            {
                File.WriteAllText(filePath, textToWrite);
            }
            catch (Exception ex)
            {
                Thread.Sleep(200);
                File.WriteAllText(filePath, textToWrite);
            }
        }
    }
}
