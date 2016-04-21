using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerIntegrationTest {
        private readonly ServerConfig _config;
        private readonly MockServer _mockServer;

        public ServerIntegrationTest() {
            var args = new string[] {};
            _config = new ServerConfig(args);
            var listener = new Listener(_config.IpAddress, _config.Port);
            var parser = new Parser();
            var handler = new DirHandler(_config.PublicDir);
            var factory = new ServiceFactory(parser, handler);
            _mockServer = new MockServer(listener, factory);
            var startTask = Task.Run(() => _mockServer.Start());
        }

        [Fact]
        public void Test() {
            while (!_mockServer.Running) {}

            var mockClient = new TcpClient();
            mockClient.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
            var rawResponse = new char[79];

            using (var stream = mockClient.GetStream()) {
                var writer = new StreamWriter(stream) {AutoFlush = true};
                var reader = new StreamReader(stream);
                var request = "GET / HTTP/1.1\r\n" +
                              "Host: localhost:5040\r\n" +
                              "Accept: */*\r\n\r\n";
                writer.Write(request);
                reader.Read(rawResponse, 0, rawResponse.Length);
            }

            Assert.Contains("HTTP/1.1 200 OK\r\n" +
                            "Content-Length: 1214\r\n\r\n", new string(rawResponse));
        }
    }
}