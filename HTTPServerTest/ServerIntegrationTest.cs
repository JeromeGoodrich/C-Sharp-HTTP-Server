using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerIntegrationTest {
        private readonly ServerConfig _config;
        private IPAddress _ip;
        private readonly MockServer _mockServer;

        public ServerIntegrationTest() {
            var args = new string[] {};
            _config = new ServerConfig(args);
            _ip = IPAddress.Any;
            var listener = new Listener(_ip, _config.Port);
            var parser = new Parser();
            var handler = new DirHandler(_config.PublicDir);
            var factory = new ServiceFactory(parser, handler);
            _mockServer = new MockServer(listener, factory);
            _mockServer.StartAsync();
        }

        [Fact]
        public void Test() {
            if (_mockServer.Running) { 
            var mockClient = new TcpClient();
            mockClient.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
            var stream = mockClient.GetStream();
            var writer = new StreamWriter(stream);
            var reader = new StreamReader(stream);
            var request = Encoding.UTF8.GetBytes("GET / HTTP/1.1\r\n" +
                                                    "Host: localhost:5039\r\n" +
                                                    "Accept: */*\r\n\r\n");
            using (writer)
            using (reader) {
                writer.Write(request);

                Assert.Equal("HTTP/1.1 200 OK\r\n" +
                             "Content-Length: 14\r\n" +
                             "Content-Type: text/plain\r\n\r\n" +
                             "file1 contents", reader.ReadToEnd());
            }
            }
        }

    }
}