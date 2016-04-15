using System.IO;
using System.Net.Sockets;
using System.Text;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerIntegrationTest {
        private readonly ServerConfig _config;

        public ServerIntegrationTest() {
            var args = new string[] {};
            _config = new ServerConfig(args);
            var listener = new Listener(_config.IpAddress, _config.Port);
            var parser = new Parser();
            var handler = new DirHandler(_config.PublicDir);
            var factory = new ServiceFactory(parser, handler);
            var mockServer = new MockServer(listener, factory);
            mockServer.Start();
        }

        [Fact]
        public void Test() {
            var mockClient = new TcpClient();
            mockClient.Connect(_config.IpAddress, _config.Port);
            var stream = mockClient.GetStream();
            var request = Encoding.UTF8.GetBytes("GET / HTTP/1.1\r\n" +
                                                 "Host: localhost:5000\r\n" +
                                                 "Accept: */*\r\n\r\n");
            stream.Write(request, 0, request.Length);
            var reader = new StreamReader(stream);

            Assert.Equal("HTTP/1.1 200 OK\r\n" +
                         "Content-Length: 14\r\n" +
                         "Content-Type: text/plain\r\n\r\n" +
                         "file1 contents", reader.ReadToEnd());
        }

    }
}