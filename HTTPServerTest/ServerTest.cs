using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        [Fact]
        public void TestServerLoop() {
            IClientSocket mockSocket = new MockSocket();
            IListener mockListener = new MockListener(mockSocket);
            var mockService = new MockService();
            IServiceFactory mockServiceFactory = new MockServiceFactory(mockService);
            var server = new Server(mockListener, mockServiceFactory);
            Assert.Equal(mockService.IsRunning(), false);
            server.Start();
            Assert.Equal(mockService.IsRunning(), true);
            Assert.Equal(mockService.GetSocket(), mockSocket);
        }
    }
}