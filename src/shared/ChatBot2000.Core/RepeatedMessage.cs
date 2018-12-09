using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot2000.Core
{
    public class RepeatedMessage
    {
        /// <summary>
        /// Delay in seconds, i.e. repeat every x seconds
        /// </summary>
        public int Delay { get; set; }
        public string Message { get; set; }
    }
}
