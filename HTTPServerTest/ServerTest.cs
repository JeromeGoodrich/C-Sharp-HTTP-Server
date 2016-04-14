using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        private MockSocket _mockSocket;
        private MockListener _mockListener;
        private MockService _mockService;
        private MockServiceFactory _mockServiceFactory;
        private Server _server;

        private void TestSetUp() {
            _mockSocket = new MockSocket();
            _mockListener = new MockListener(_mockSocket);
            _mockService = new MockService();
            _mockServiceFactory = new MockServiceFactory(_mockService);
            _server = new Server(_mockListener, _mockServiceFactory);
        }

        [Fact]
        public void TestServiceIsNotRunningBeforeStartingServer() {
            TestSetUp();
            Assert.Equal(_mockService.IsRunning(), false);
        }

        [Fact]
        public void TestServiceIsRunningAfterStartingServer() {
            TestSetUp();
            _server.Start();
            Assert.Equal(_mockService.IsRunning(), true);
        }

        [Fact]
        public void TestServiceIsPassedSocket()
        {
            TestSetUp();
            _server.Start();
            Assert.Equal(_mockService.GetSocket(), _mockSocket);
        }
    }
}