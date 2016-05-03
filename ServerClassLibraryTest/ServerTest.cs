using System.Threading;
using System.Threading.Tasks;
using ServerClassLibrary;
using ServerClassLibraryTest.Mocks;
using Xunit;

namespace ServerClassLibraryTest {
    public class ServerTest {
        private readonly MockRequestProcessor _mockRequestProcessor;
        private readonly MockSocket _mockSocket;
        private readonly Server _server;
        private readonly CancellationTokenSource _tokenSource;
        private readonly MockListener _mockListener;

        public ServerTest() {
            _tokenSource = new CancellationTokenSource();
            _mockSocket = new MockSocket();
            _mockListener = new MockListener(_mockSocket);
            _mockRequestProcessor = new MockRequestProcessor();
            var mockRequestProcessorFactory = new MockRequestProcessorFactory(_mockRequestProcessor);
            _server = new Server(_mockListener, mockRequestProcessorFactory);
        }

        private void StartServer() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));
            _tokenSource.Cancel();
            task.Wait();
        }

        [Fact]
        public void ListenerHasNotStartedBeforeStartingServer() {
            Assert.Equal(false, _mockListener.IsListening());
        }

        [Fact]
        public void RequestProcessorIsNotRunningBeforeStartingServer() {
            Assert.Equal(false, _mockRequestProcessor.IsRunning());
        }

        [Fact]
        public void ListenerStartsAfterStartingServer() {
            StartServer();
            Assert.Equal(true, _mockListener.IsListening());
        }

        [Fact]
        public void RequestProcessorRunsAfterStartingServer() {
            StartServer();
            Assert.Equal(true, _mockRequestProcessor.IsRunning());
        }

        [Fact]
        public void TestRequestProcessorIsPassedSocket() {
            StartServer();
            Assert.Equal(_mockRequestProcessor.Socket, _mockSocket);
        }

        [Fact]
        public void TaskThatStartsServerCompletesAfterCancellation() {
            var task = Task.Run(() => _server.Start(_tokenSource.Token));

            Assert.False(task.IsCompleted);

            _tokenSource.Cancel();
            task.Wait();

            Assert.True(task.IsCompleted);
        }
    }
}