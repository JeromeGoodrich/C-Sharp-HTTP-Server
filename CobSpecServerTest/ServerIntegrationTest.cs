using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ServerClassLibrary;
using CobSpecServer;
using Xunit;
using System.Reflection;
using System.Diagnostics;
using System;

namespace CobSpecServerTest {
    public class ServerIntegrationTest {
        private readonly CommandLineConfig _config;
        private readonly Server _server;
        private readonly CancellationTokenSource _tokenSource;
        private const string Request = "GET / HTTP/1.1\r\n" +
                                           "Host: localhost:5000\r\n" +
                                           "Accept: */*\r\n\r\n";
        private string _publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures");
        private string _logFile = Path.Combine(Environment.CurrentDirectory, @"..\..\..\logs\testlog.txt");

        public ServerIntegrationTest() {
            var args = new string[] {"-d", _publicDir, "-l", _logFile};
            _config = new CommandLineConfig(args);
            _tokenSource = new CancellationTokenSource();
            var listener = new Listener(_config.IpAddress, _config.Port);
            var parser = new Parser();
            var router = new Router();
            router.AddRoute(new Route("GET", "/", new DirHandler(_config.PublicDir)));
            var factory = new RequestProcessorFactory(parser, router);
            _server = new Server(listener, factory);
             var startTask = Task.Run(() => _server.Start(_tokenSource.Token));
            
        }

        private char[] MakeRequest(TcpClient client, char[] responseContainer) {
            client.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
            using (var stream = client.GetStream()) {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                var reader = new StreamReader(stream);

                writer.Write(Request);
                reader.Read(responseContainer, 0, responseContainer.Length);
            }
            return responseContainer;
        }

        [Fact]
        public void MultipleSimultaneousRequestsTest() {
            var clientList = new ArrayList(1500);
            for (var i = 0; i < clientList.Capacity; i++) {
                clientList.Add(new TcpClient());
            }

            var responses = new ArrayList();
            var responseContainer = new char[17];
            foreach(TcpClient client in clientList) {
                var task = Task.Run(() => MakeRequest(client, responseContainer));
                var response = task.Result;
                responses.Add(response);
            }

            _tokenSource.Cancel();
            Assert.Equal(1500, responses.Count);
            foreach (char[] response in responses) {
                Assert.Equal("HTTP/1.1 200 OK\r\n", new string(response));
            }
        }
    }
}