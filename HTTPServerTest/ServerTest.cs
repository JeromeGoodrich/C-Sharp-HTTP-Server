using System;
using System.Threading;
using System.Threading.Tasks;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        private readonly MockService _mockService;
        private readonly MockSocket _mockSocket;
        private readonly Server _server;
        private readonly CancellationTokenSource _tokenSource;
        private readonly MockListener _mockListener;

        public ServerTest() {
            _tokenSource = new CancellationTokenSource();
            _mockSocket = new MockSocket();
            _mockListener = new MockListener(_mockSocket);
            _mockService = new MockService();
            var mockServiceFactory = new MockServiceFactory(_mockService);
            _server = new Server(_mockListener, mockServiceFactory);
        }

        [Fact]
        public void ListenerHasNotStartedBeforeStartingServer() {
            Assert.Equal(false, _mockListener.IsListening());
        }

        [Fact]
        public void ServiceIsNotRunningBeforeStartingServer() {
            Assert.Equal(false, _mockService.IsRunning());
        }

        [Fact]
        public void ListenerStartsAfterStartingServer() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));
            _tokenSource.Cancel();
            task.Wait();
            Assert.Equal(true, _mockListener.IsListening());
        }

        [Fact]
        public void ServiceRunsAfterStartingServer() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));
            _tokenSource.Cancel();
            task.Wait();
            Assert.Equal(true, _mockService.IsRunning());
        }

        [Fact]
        public void TestServiceIsPassedSocket() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));
            _tokenSource.Cancel();
            task.Wait();
            Assert.Equal(_mockService.Socket, _mockSocket);
        }

        [Fact]
        public void TaskThatStartsServerCompletesAfterCancellation() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));

            Assert.Equal(false, task.IsCompleted);

            _tokenSource.Cancel();
            task.Wait();

            Assert.Equal(true, task.IsCompleted);
        }
    }
}