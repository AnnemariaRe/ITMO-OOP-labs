using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : ILogger
    {
        void ILogger.Log(EnumLogType logType, string message)
        {
            Console.WriteLine($"{DateTime.Now} | {logType}: {message}");
        }
    }
}