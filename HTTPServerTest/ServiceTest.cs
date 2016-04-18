using Xunit;
using HTTPServer;
using System.IO;
using HTTPServerTest.Mocks;

namespace HTTPServerTest {

    public class ServiceTest {
        private readonly MemoryStream _ioStream;
        private readonly MockSocket _mockSocket;
        private readonly Request _request;
        private readonly MockParser _mockParser;
        private readonly MockResponse _mockResponse;
        private readonly MockHandler _mockHandler;
        private readonly Service _service;


        public ServiceTest() {
            _ioStream = new MemoryStream(new byte[1024]);
            _mockSocket = new MockSocket(_ioStream);
            _request = new Request();
            _mockParser = new MockParser(_request);
            _mockResponse = new MockResponse();
            _mockHandler = new MockHandler(_mockResponse);
            _service = new Service(_mockSocket, _mockParser, _mockHandler);
        }

        [Fact]
        public void TestParseIsNotCalledBeforeRunningService() {
            Assert.Equal(_mockParser.GetCallsToParse(), 0);
        }

        [Fact]
        public void TestHandleIsNotCalledBeforeRunningService() {
            Assert.Equal(_mockHandler.GetCallsToHandle(), 0);
        }

        [Fact]
        public void TestSendIsNotCalledBeforeRunningService() {
            Assert.Equal(_mockResponse.GetCallsToSend(), 0);
        }

        [Fact]
        public void TestSocketIsOpenBeforeRunningService() {
            Assert.Equal(_mockSocket.IsClosed(), false);
        }

        [Fact]
        public void TestParseIsCalledAfterRunningService() {
            _service.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestParseIsCalledWithStream() {
            _service.Run();
            Assert.Equal(_mockParser.GetLastStreamPassedToParse(), _ioStream);
        }

        [Fact]
        public void TestParseReturnsRequest() {
            Assert.Equal(_mockParser.Parse(new StreamReader(_mockSocket.GetStream())), _request);
        }

        [Fact]
        public void TestHandleIsCalledAfterRunningService() {
            _service.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestHandleIsCalledWithRequest() {
            _service.Run();
            Assert.Equal(_mockHandler.GetLastRequestPassedToHandle(), _request);
        }

        [Fact]
        public void TestHandleReturnsResponse() {
            _service.Run();
            Assert.Equal(_mockHandler.Handle(_request), _mockResponse);
        }

        [Fact]
        public void TestSendIsCalledAfterRunningService() {
            _service.Run();
            Assert.Equal(_mockResponse.GetCallsToSend(), 1);
        }

        [Fact]
        public void TestSendIsCalledWithStream() {
            _service.Run();
            Assert.Equal(_mockResponse.GetLastStreamPassedToSend(), _ioStream);
        }

        [Fact]
        public void TestSocketClosesAfterRunningService() {
            _service.Run();
            Assert.Equal(true, _mockSocket.IsClosed());
        }
    }
}