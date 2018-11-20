using System;

namespace No8.Solution.Models
{
    /// <summary>
    /// Class which contains data.
    /// </summary>
    public class DataEventArgs : EventArgs
    {
        /// <summary>
        /// Message for subscribers.
        /// </summary>
        public string Message { get; set; }
    }
}
