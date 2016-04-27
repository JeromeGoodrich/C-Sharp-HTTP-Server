using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ServerIntegrationTest {
        private readonly ServerConfig _config;
        private readonly Server _server;
        private readonly CancellationTokenSource _tokenSource;
        private Task _startTask;

        public ServerIntegrationTest() {
            var args = new string[] {};
            _config = new ServerConfig(args);
            _tokenSource = new CancellationTokenSource();
            var listener = new Listener(_config.IpAddress, _config.Port);
            var parser = new Parser();
            var handler = new Router(_config.PublicDir);
            var factory = new ServiceFactory(parser, handler);
            _server = new Server(listener, factory);
            _startTask = Task.Run(() => _server.Start(_tokenSource.Token));
            
        }

        [Fact]
        public void Test() {
            using (var mockClient = new TcpClient()) {
                mockClient.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
                var rawResponse = new char[79];

                _tokenSource.Cancel();


                using (var stream = mockClient.GetStream()) {
                    var writer = new StreamWriter(stream) {AutoFlush = true};
                    var reader = new StreamReader(stream);
                    var request = "GET / HTTP/1.1\r\n" +
                                  "Host: localhost:5000\r\n" +
                                  "Accept: */*\r\n\r\n";

                    writer.Write(request);
                    reader.Read(rawResponse, 0, rawResponse.Length);

                }
                Assert.Contains("HTTP/1.1 200 OK\r\n" +
                                "Content-Length: 554\r\n\r\n", new string(rawResponse));
            }
        }
    }
}