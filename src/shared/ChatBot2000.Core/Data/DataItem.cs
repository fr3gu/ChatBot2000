using System;
using ChatBot2000.Core.Messaging;

namespace ChatBot2000.Core.Data
{
    public abstract class DataItem
    {
        public Guid Id { get; set; }
        public DataItemStatus DataItemStatus { get; protected set; } = DataItemStatus.Draft;
    }
}
