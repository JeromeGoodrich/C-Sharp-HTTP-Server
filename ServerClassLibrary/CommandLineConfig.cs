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
        public FileLogger Logger { get;  }

        public CommandLineConfig(string[] args) {
            Config(args);
        }

        public void Config(string[] args) {
            SetPort(args);
            SetPublicDir(args);
        }

        private void SetPort(params string[] args) {
            if (args.Contains("-p")) {
                var portIndex = Array.IndexOf(args, "-p") + 1;
                Port = args[portIndex] != null ? int.Parse(args[portIndex]) : DefaultPort;
            }
            else {
                Port = DefaultPort;
            }
        }

        private void SetPublicDir(params string[] args) {
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