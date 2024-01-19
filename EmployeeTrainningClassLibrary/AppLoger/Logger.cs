using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.AppLoger
{
    public class Logger: ILogger
    {
        private readonly string logFilePath;
        public Logger(string FilePath = "")
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            logFilePath = Path.Combine(baseDirectory, FilePath);
        }
        public void Log(string message)
        {
            string logMessage = $"{message}";
            try
            {
                if (!File.Exists(logFilePath))
                {
                    File.Create(logFilePath);
                }
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file{ex.Message}");
            }
        }

        public void Log(Exception exception)
        {
            string fullMessage = "--------------------------------------------------";
            fullMessage += Environment.NewLine + $"Timestamp: {DateTime.Now}";
            fullMessage += Environment.NewLine + $"Exception Type: {exception.GetType().FullName}";
            fullMessage += Environment.NewLine + $"Message: {exception.Message}";
            fullMessage += Environment.NewLine + $"Inner Exception: {exception.InnerException}";
            fullMessage += Environment.NewLine + $"Stack Trace: {exception.StackTrace}";
            fullMessage += Environment.NewLine + "--------------------------------------------------";
            Log(fullMessage);
        }
    }
}
