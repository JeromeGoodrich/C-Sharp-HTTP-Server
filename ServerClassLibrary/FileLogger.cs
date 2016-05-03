using System.IO;

namespace ServerClassLibrary {
    public class FileLogger {
        private string _logFile;

        public FileLogger(string logFile) {
            _logFile = logFile;
        }

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