using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        private readonly MockSocket _mockSocket;
        private readonly MockService _mockService;
        private readonly Server _server;

        public ServerTest() {
            _mockSocket = new MockSocket();
            var mockListener = new MockListener(_mockSocket);
            _mockService = new MockService();
            var mockServiceFactory = new MockServiceFactory(_mockService);
            _server = new Server(mockListener, mockServiceFactory);
        }

        [Fact]
        public void TestServiceIsNotRunningBeforeStartingServer() {
            Assert.Equal(_mockService.IsRunning(), false);
        }

        [Fact]
        public async void TestServiceIsRunningAfterStartingServer() {
            await _server.StartAsync();
            Assert.Equal(_mockService.IsRunning(), true);
        }

        [Fact]
        public async void TestServiceIsPassedSocket() {
            await _server.StartAsync();
            Assert.Equal(_mockService.Socket, _mockSocket);
        }
    }
}