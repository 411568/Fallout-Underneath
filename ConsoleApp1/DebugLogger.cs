using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace FalloutUnderneath
{
    static class DebugLogger
    {
        private static string _filePath = "DebugLog.txt";
        
        // Set the path to the debug file
        public static void SetDebugFile(string logPath)
        {
            _filePath = logPath;
            ClearLogFile();
        }

        // Write to log file
        public static void Log(string input)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                string callingMethod = GetCallingMethod();
                writer.WriteLine($"{DateTime.Now}: {callingMethod}  Log message: {input}");
            }
        }

        // Write to log file
        public static void LogError(string input)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                string callingMethod = GetCallingMethod();
                writer.WriteLine($"{DateTime.Now}: {callingMethod}  ERROR message: {input}");
            }
        }

        // Get the name of the method from which the log function is called
        private static string GetCallingMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame? frame = stackTrace.GetFrame(2);

            if (frame == null)
            {
                return "UnknownMethod";
            }

            var method = frame.GetMethod();
            if (method == null)
            {
                return "UnknownMethod";
            }

            string className = method.DeclaringType?.FullName ?? "UnknownClass";
            string methodName = method.Name ?? "UnknownMethod";

            return $"{className}.{methodName}";
        }
        
        // Clear the file by opening it with FileMode.Create
        private static void ClearLogFile()
        {
            using (FileStream stream = new FileStream(_filePath, FileMode.Create))
            {
                // Immediately close the file to truncate it
            }
        }
    }
}