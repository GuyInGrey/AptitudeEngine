using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Text;

namespace AptitudeEngine.Logger
{
    public static class LoggingHandler
    {
        public static List<LogMessage> Messages { get; private set; }
        public static List<LogMessageType> ConsoleLogBlacklist { get; set; }
        public static string CurrentlyWritingLogFile = "";
        private static bool Ready = false;
        private static FileStream LogFileStream;

        internal static void Boot()
        {
            var a = DateTime.Now;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Logs"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs");
            }

            CurrentlyWritingLogFile = (@"\Logs\" + a + ".log.txt")
                .Replace("/", "-")
                .Replace(":", "-");

            CurrentlyWritingLogFile = Directory.GetCurrentDirectory() + CurrentlyWritingLogFile;



            LogFileStream = File.Create(CurrentlyWritingLogFile);
            Messages = new List<LogMessage>();
            ConsoleLogBlacklist = new List<LogMessageType>();

            Ready = true;
            InternalLogMessage("LOG FILE STARTING", LogMessageType.Info);
        }

        internal static void InternalLogMessage(object content, LogMessageType type) => _LogMessage(content, type, LogMessageSource.Engine);
        
        public static void LogMessage(object content, LogMessageType type) => _LogMessage(content, type, LogMessageSource.Game);

        private static void _LogMessage(object content, LogMessageType type, LogMessageSource source)
        {
            while (!Ready)
            {
                
            }

            var trace = new StackTrace(2, true);
            var frame = new StackFrame(2, true);
            var timeSent = DateTime.Now;
            var tpFinal = new LogMessage(content, type, source, timeSent, trace, frame);
            
            Messages.Add(tpFinal);

            var finalOutConsole = "[" + timeSent.ToString() + "]:" +
                frame.GetFileLineNumber() +
                ": " + frame.GetMethod().ReflectedType.Name + "." + frame.GetMethod().Name +
                " >>";

            var finalOutFile = finalOutConsole + content.ToString();

            var info = new UTF8Encoding(true).GetBytes(finalOutFile);
            LogFileStream.Write(info, 0, info.Length);

            if (!ConsoleLogBlacklist.Contains(type))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(finalOutConsole);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + content.ToString() + "\n");
            }
        }
    }
}