using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Text;
using AptitudeEngine;

namespace AptitudeEngine.Logger
{
    public static class LoggingHandler
    {
        private static List<LogMessage> Messages { get; set; }
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
            IntLog("LOG FILE STARTING", LogMessageType.Info);
        }

        public static LogMessage[] LogMessages => Messages.ToArray();

        internal static void IntLog(object content, LogMessageType type) => _LogMessage(content, type, LogMessageSource.Engine);
        
        public static void ExtLog(object content, LogMessageType type) => _LogMessage(content, type, LogMessageSource.Game);

        private static void _LogMessage(object content, LogMessageType type, LogMessageSource source)
        {
            while (!Ready)
            {
                
            }

            Ready = false;

            var trace = new StackTrace(2, true);
            var frame = new StackFrame(2, true);
            var timeSent = DateTime.Now;
            var tpFinal = new LogMessage(content, type, source, timeSent, trace, frame);
            
            Messages.Add(tpFinal);

            var finalOutConsole = "[" + timeSent.ToString() + "]{" + source + "}{" + type + "}:" +
                frame.GetFileLineNumber() +
                ": " + frame.GetMethod().ReflectedType.Name + "." + frame.GetMethod().Name + "()" +
                " >>";

            var finalOutFile = finalOutConsole + content.ToString() + "\n";

            var info = new UTF8Encoding(true).GetBytes(finalOutFile);
            LogFileStream.WriteAsync(info, 0, info.Length);
            LogFileStream.FlushAsync();

            if (!ConsoleLogBlacklist.Contains(type))
            {
                var foreColor = ConsoleColor.White;

                switch (type)
                {
                    case (LogMessageType)1:
                        foreColor = ConsoleColor.Cyan;
                        break;
                    case (LogMessageType)2:
                        foreColor = ConsoleColor.Green;
                        break;
                    case (LogMessageType)3:
                        foreColor = ConsoleColor.DarkYellow;
                        break;
                    case (LogMessageType)4:
                        foreColor = ConsoleColor.Yellow;
                        break;
                    case (LogMessageType)5:
                        foreColor = ConsoleColor.Red;
                        break;
                    case (LogMessageType)6:
                        foreColor = ConsoleColor.Magenta;
                        break;
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = foreColor;
                Console.Write(finalOutConsole);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = foreColor;
                Console.Write(" " + content.ToString() + "\n");
            }

            Ready = true;
        }
    }
}