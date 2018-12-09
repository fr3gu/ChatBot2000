using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot2000.Core
{
    public class RepeatingMessage
    {
        /// <summary>
        /// Delay in seconds, i.e. repeat every x seconds
        /// </summary>
        public int RepeatEvery { get; set; }
        public string Message { get; set; }
    }
}
