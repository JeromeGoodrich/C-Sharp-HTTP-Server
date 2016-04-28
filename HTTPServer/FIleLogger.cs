using System;
using System.IO;

namespace HTTPServer {
    public class FileLogger {
        public void Log(string logMessage) {
            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HTTPServer\logFile.txt"),
                true)) {
                writer.Write(logMessage);
            }
        }
    }
}