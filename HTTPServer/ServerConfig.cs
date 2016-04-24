using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HTTPServer {
    public class ServerConfig {
        private const int DefaultPort = 5000;

        private readonly string _defaultPublicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures").Normalize();

        public IPAddress IpAddress = IPAddress.Any;
        public int Port { get; private set; }
        public string PublicDir { get; private set; }
        public List<IHandler> Handlers = new List<IHandler>();


        public ServerConfig(string[] args) {
            Config(args);
        }

        public void Config(string[] args) {
            SetPort(args);
            SetPublicDir(args);
            CreateHandlers();
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

        private void CreateHandlers() {
            Handlers.Add(new DirHandler(PublicDir));
            Handlers.Add(new BasicAuthHandler());
            Handlers.Add(new FileHandler(PublicDir));
            Handlers.Add(new NotFoundHandler());
            
        }
    }
}