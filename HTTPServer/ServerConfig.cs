using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HTTPServer {
    public class ServerConfig {

        public int Port { get; private set; }
        public string PublicDir { get; private set; }
        public IPAddress IpAddress = IPAddress.Any;
        private const int DefaultPort = 5000;
        private readonly string _defaultPublicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\..\HTTPServerTest\Fixtures\");

        public ServerConfig(string[] args) {
            Config(args);
        }

        public void Config(string[] args) {
            SetPort(args);
            SetPublicDir(args);
            SetIpAddress();
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

        private void SetIpAddress() {
            IPAddress localIp = null;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) {
               if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    localIp = ip;
                }
            }
            IpAddress = IPAddress.Any;
        }
    }
}