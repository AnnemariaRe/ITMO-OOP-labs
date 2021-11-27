using System;
using System.IO;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Logger
{
    public class FileLogger : ILogger
    {
        void ILogger.Log(EnumLogType logType, string message)
        {
            File.AppendAllText("BackupsExtraLog.txt", $"{DateTime.Now} | {logType}: {message}");
        }
    }
}