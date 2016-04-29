using System;
using System.IO;

namespace HTTPServer {
    public class FileLogger {
        private readonly string _logFile = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HTTPServer\Logs\logFile.txt");

        public void Log(string logMessage) {
            if (!File.Exists(_logFile)) {
                new FileStream(_logFile, FileMode.CreateNew).Dispose();
            }
            using (var writer = new StreamWriter(_logFile, true)) {
                writer.Write(logMessage);
            }
        }
    }
}