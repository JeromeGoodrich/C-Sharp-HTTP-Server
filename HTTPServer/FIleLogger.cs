using System;
using System.IO;
using System.Reflection;

namespace HTTPServer {
    public class FileLogger {
        private readonly string _logFile = Assembly.GetExecutingAssembly()+ @"logFile.txt";

        public void Log(string logMessage) {
            if (!File.Exists(_logFile)) {
                new FileStream(_logFile, FileMode.CreateNew).Dispose();
            }
            using (var writer = new StreamWriter(_logFile, true)) {
                writer.Write(logMessage);
            }
        }
    }
} ;