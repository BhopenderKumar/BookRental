using NLog;

namespace BookRental.Infrastructure.Logging
{
    public static class LoggingHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogException(Exception ex)
        {
            if (ex != null)
            {
                logger.Error(ex, "An exception occurred");
            }
        }
        public static void LogInfo(string message)
        {
            logger.Info(message);
        }

        public static void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public static void LogError(string message)
        {
            logger.Error(message);
        }

        public static void LogDebug(string message)
        {
            logger.Debug(message);
        }
    }

}
