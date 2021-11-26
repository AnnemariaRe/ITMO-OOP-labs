using BackupsExtra.Logger;

namespace BackupsExtra.Interfaces
{
    public interface ILogger
    {
        protected void Log(EnumLogType logType, string message);

        public void Debug(string message)
        {
            Log(EnumLogType.Debug, message);
        }

        public void Warning(string message)
        {
            Log(EnumLogType.Warning, message);
        }

        public void Error(string message)
        {
            Log(EnumLogType.Error, message);
        }

        public void Info(string message)
        {
            Log(EnumLogType.Info, message);
        }
    }
}