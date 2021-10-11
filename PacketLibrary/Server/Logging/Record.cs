using System;
using System.Text;

namespace PacketLibrary.Logging
{
    public class Record
    {

        private Level LogLevel { get; }
        private string Message { get; }
        private DateTime Time { get; }
        private Exception Exception { get; }

        public Record(Level level, string message)
        {
            Message = message;
            LogLevel = level;

            Time = DateTime.Now;
        }

        public Record(Level level, string message, DateTime time)
        {
            LogLevel = level;
            Message = message;
            Time = time;
        }

        public Record(Level level, string message, DateTime time, Exception exception)
        {
            LogLevel = level;
            Message = message;
            Time = time;
            Exception = exception;
        }

        public string GetFormatted()
        {
            StringBuilder formated = new StringBuilder("[" + Time.ToString() + "] " + "[" + LogLevel.ToString().ToUpperInvariant() + "] ");
            if (Exception != null)
            {
                formated.Append(Exception.GetType().FullName).Append(" ");
            }

            formated.Append(Message);

            if (Exception != null)
            {
                formated.Append(Exception.StackTrace);
            }

            return formated.ToString();
        }

        public enum Level
        {
            DEBUG, INFO, WARN, ERROR
        }
    }
}
