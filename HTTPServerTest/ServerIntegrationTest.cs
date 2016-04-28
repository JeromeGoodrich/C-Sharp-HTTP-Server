using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private const string Request = "GET / HTTP/1.1\r\n" +
                                           "Host: localhost:5000\r\n" +
                                           "Accept: */*\r\n\r\n";

        public ServerIntegrationTest() {
            var args = new string[] {};
            _config = new ServerConfig(args);
            _tokenSource = new CancellationTokenSource();
            var listener = new Listener(_config.IpAddress, _config.Port);
            var parser = new Parser();
            var handler = new Router(_config.PublicDir);
            var factory = new RequestProcessorFactory(parser, handler);
            _server = new Server(listener, factory);
             var startTask = Task.Run(() => _server.Start(_tokenSource.Token));
            
        }

        [Fact]
        public void ServerReceivesRequestAndReturnsExpecetedResponse() {
            using (var mockClient = new TcpClient()) {
                mockClient.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
                var rawResponse = new char[79];

                _tokenSource.Cancel();

                using (var stream = mockClient.GetStream()) {
                    var writer = new StreamWriter(stream) {AutoFlush = true};
                    var reader = new StreamReader(stream);

                    writer.Write(Request);
                    reader.Read(rawResponse, 0, rawResponse.Length);
                }
                Assert.Contains("HTTP/1.1 200 OK\r\nContent-Length: 554\r\n\r\n", new string(rawResponse));
            }
        }

        private char[] MakeRequest(TcpClient client, char[] responseContainer) {
            client.Connect(IPAddress.Parse("127.0.0.1"), _config.Port);
            using (var stream = client.GetStream())
            {
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
            foreach (var response in from TcpClient client in clientList select 
                     Task.Run(() => MakeRequest(client, responseContainer)) 
                     into task select task.Result) {
                responses.Add(response);
            }
            _tokenSource.Cancel();
            foreach (char[] response in responses) {
                Assert.Equal("HTTP/1.1 200 OK\r\n", new string(response));
            }
        }
    }
}