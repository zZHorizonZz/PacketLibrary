using System;
using System.Collections.Generic;

namespace PacketLibrary.Logging
{
    public class Logger
    {

        public static Logger LOGGER = new Logger();

        private LinkedList<Record> RecordLog { get; }

        public Logger()
        {
            RecordLog = new LinkedList<Record>();
        }

        public Record Debug(string message)
        {
            return Debug(message, null);
        }

        public Record Debug(string message, object[] arguments)
        {
            return Log(Record.Level.DEBUG, message, null, arguments);
        }
        public Record Info(string message)
        {
            return Info(message, null);
        }

        public Record Info(string message, object[] arguments)
        {
            return Log(Record.Level.INFO, message, null, arguments);
        }
        public Record Warn(string message)
        {
            return Warn(message, null);
        }

        public Record Warn(string message, object[] arguments)
        {
            return Log(Record.Level.WARN, message, null, arguments);
        }

        public Record Error(string message, Exception exception)
        {
            return Error(message, exception, null);
        }

        public Record Error(string message, Exception exception, object[] arguments)
        {
            return Log(Record.Level.ERROR, message, exception, arguments);
        }

        public Record Log(Record.Level level, string message, Exception exception, object[] arguments)
        {
            message = string.Format(message, arguments);

            Record record = new Record(level, message, DateTime.Now, exception);
            RecordLog.AddLast(record);

            Console.WriteLine(record.GetFormatted(), arguments);

            return record;
        }
    }
}
