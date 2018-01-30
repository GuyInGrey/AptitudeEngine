﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AptitudeEngine.Logging;
using AptitudeEngine.Logging.Handlers;
using AptitudeEngine.Logging.Modules;

namespace AptitudeEngine
{
    public static class Logger
    {
        private static readonly LogPublisher logPublisher;
        private static readonly ModuleManager moduleManager;
        private static readonly DebugLogger debugLogger;

        private static readonly object sync = new object();
        private static Level _defaultLevel = Level.Info;
        private static bool _isTurned = true;
        private static bool _isTurnedDebug = true;

        public enum Level
        {
            None,
            Debug,
            Fine,
            Info,
            Warning,
            Error,
            Severe,
        }

        static Logger()
        {
            lock (sync)
            {
                logPublisher = new LogPublisher();
                moduleManager = new ModuleManager();
                debugLogger = new DebugLogger();
            }
        }

        public static void DefaultInitialization()
        {
            LoggerHandlerManager
                .AddHandler(new ConsoleLoggerHandler())
                .AddHandler(new FileLoggerHandler());

            Log(Level.Info, "Default initialization");
        }

        public static Level DefaultLevel
        {
            get => _defaultLevel;
            set => _defaultLevel = value;
        }

        public static ILoggerHandlerManager LoggerHandlerManager => logPublisher;

        public static void Log() => Log("There is no message");

        public static void Log(string message) => Log(_defaultLevel, message);

        public static void Log(Level level, string message)
        {
            var stackFrame = FindStackFrame();
            var methodBase = GetCallingMethodBase(stackFrame);
            var callingMethod = methodBase.Name;
            var callingClass = methodBase.ReflectedType.Name;
            var lineNumber = stackFrame.GetFileLineNumber();

            Log(level, message, callingClass, callingMethod, lineNumber);
        }

        public static void Log(Exception exception)
        {
            Log(Level.Error, exception.Message);
            moduleManager.ExceptionLog(exception);
        }

        public static void Log<TClass>(Exception exception) where TClass : class
        {
            var message = string.Format("Log exception -> Message: {0}\nStackTrace: {1}", exception.Message,
                exception.StackTrace);
            Log<TClass>(Level.Error, message);
        }

        public static void Log<TClass>(string message) where TClass : class => Log<TClass>(_defaultLevel, message);

        public static void Log<TClass>(Level level, string message) where TClass : class
        {
            var stackFrame = FindStackFrame();
            var methodBase = GetCallingMethodBase(stackFrame);
            var callingMethod = methodBase.Name;
            var callingClass = typeof(TClass).Name;
            var lineNumber = stackFrame.GetFileLineNumber();

            Log(level, message, callingClass, callingMethod, lineNumber);
        }

        private static void Log(Level level, string message, string callingClass, string callingMethod, int lineNumber)
        {
            if (!_isTurned || (!_isTurnedDebug && level == Level.Debug))
            {
                return;
            }

            var currentDateTime = DateTime.Now;

            moduleManager.BeforeLog();
            var logMessage = new LogMessage(level, message, currentDateTime, callingClass, callingMethod, lineNumber);
            logPublisher.Publish(logMessage);
            moduleManager.AfterLog(logMessage);
        }

        private static MethodBase GetCallingMethodBase(StackFrame stackFrame)
        => stackFrame == null ? MethodBase.GetCurrentMethod() : stackFrame.GetMethod();

        private static StackFrame FindStackFrame()
        {
            var stackTrace = new StackTrace();
            for (var i = 0; i < stackTrace.GetFrames().Count(); i++)
            {
                var methodBase = stackTrace.GetFrame(i).GetMethod();
                var name = MethodBase.GetCurrentMethod().Name;
                if (!methodBase.Name.Equals("Log") && !methodBase.Name.Equals(name))
                {
                    return new StackFrame(i, true);
                }
            }

            return null;
        }

        public static void On() => _isTurned = true;

        public static void Off() => _isTurned = false;

        public static void DebugOn() => _isTurnedDebug = true;

        public static void DebugOff() => _isTurnedDebug = false;

        public static IEnumerable<LogMessage> Messages => logPublisher.Messages;

        public static DebugLogger Debug => debugLogger;

        public static ModuleManager Modules => moduleManager;

        public static bool StoreLogMessages
        {
            get => logPublisher.StoreLogMessages;
            set => logPublisher.StoreLogMessages = value;
        }

        static class FilterPredicates
        {
            public static bool ByLevelHigher(Level logMessLevel, Level filterLevel) => ((int)logMessLevel >= (int)filterLevel);

            public static bool ByLevelLower(Level logMessLevel, Level filterLevel) => ((int)logMessLevel <= (int)filterLevel);

            public static bool ByLevelExactly(Level logMessLevel, Level filterLevel) => ((int)logMessLevel == (int)filterLevel);

            public static bool ByLevel(LogMessage logMessage, Level filterLevel, Func<Level, Level, bool> filterPred) => filterPred(logMessage.Level, filterLevel);
        }

        public class FilterByLevel
        {
            public Level FilteredLevel { get; set; }
            public bool ExactlyLevel { get; set; }
            public bool OnlyHigherLevel { get; set; }

            public FilterByLevel(Level level)
            {
                FilteredLevel = level;
                ExactlyLevel = true;
                OnlyHigherLevel = true;
            }

            public FilterByLevel()
            {
                ExactlyLevel = false;
                OnlyHigherLevel = true;
            }

            public Predicate<LogMessage> Filter => delegate (LogMessage logMessage)                 
                                                    {                 
                                                        return FilterPredicates.ByLevel(logMessage, FilteredLevel, delegate (Level lm, Level fl)                 
                                                        {                 
                                                            return ExactlyLevel                 
                                                                ? FilterPredicates.ByLevelExactly(lm, fl)                 
                                                                : (OnlyHigherLevel                 
                                                                    ? FilterPredicates.ByLevelHigher(lm, fl)                 
                                                                    : FilterPredicates.ByLevelLower(lm, fl)                 
                                                                );                 
                                                        });                 
                                                    };                 
        }
    }
}