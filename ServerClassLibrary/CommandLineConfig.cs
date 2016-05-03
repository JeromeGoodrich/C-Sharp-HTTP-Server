using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ServerClassLibrary {
    public class CommandLineConfig {
        private const int DefaultPort = 5000;

        private readonly string _defaultPublicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures\");

        public IPAddress IpAddress = IPAddress.Any;
        public int Port { get; private set; }
        public string PublicDir { get; private set; }
        public List<IHandler> Handlers = new List<IHandler>();
        public FileLogger Logger { get; }
        public string LogFile { get; set; }

        public CommandLineConfig(string[] args) {
            Config(args);
        }

        public void Config(string[] args) {
            SetPort(args);
            SetPublicDir(args);
            SetLogFile(args);
        }

        private void SetLogFile(string[] args) {
            if (args.Contains("-l")) {
                var logFileIndex = Array.IndexOf(args, "-l") + 1;
                LogFile = args[logFileIndex];
            }
        }

        private void SetPort(string[] args) {
            if (args.Contains("-p")) {
                var portIndex = Array.IndexOf(args, "-p") + 1;
                Port = args[portIndex] != null ? int.Parse(args[portIndex]) : DefaultPort;
            }
            else {
                Port = DefaultPort;
            }
        }

        private void SetPublicDir(string[] args) {
            if (args.Contains("-d")) {
                var dirIndex = Array.IndexOf(args, "-d") + 1;
                PublicDir = args[dirIndex] ?? _defaultPublicDir;
            }
            else {
                PublicDir = _defaultPublicDir;
            }
        }
    }
}