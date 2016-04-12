using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        [Fact]
        public void TestServerLoop() {
            var mockSocket = new MockSocket();
            var mockListener = new MockListener(mockSocket);
            var mockService = new MockService();
            var mockServiceFactory = new MockServiceFactory(mockService);
            var server = new Server(mockListener, mockServiceFactory);

            Assert.Equal(mockService.IsRunning(), false);
            server.Start();
            Assert.Equal(mockService.IsRunning(), true);
            Assert.Equal(mockService.GetSocket(), mockSocket);
        }
    }
}