using System.IO;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class RequestProcessorTest {
        private readonly MemoryStream _ioStream;
        private readonly MockHandler _mockHandler;
        private readonly MockParser _mockParser;
        private readonly MockResponse _mockResponse;
        private readonly MockSocket _mockSocket;
        private readonly Request _request;
        private readonly RequestProcessor _requestProcessor;


        public RequestProcessorTest() {
            _ioStream = new MemoryStream(new byte[1024]);
            _mockSocket = new MockSocket(_ioStream);
            _request = new Request();
            _mockParser = new MockParser(_request);
            _mockResponse = new MockResponse();
            _mockHandler = new MockHandler(_mockResponse);
            var mockRouter = new MockRouter(_mockHandler);
            _requestProcessor = new RequestProcessor(_mockSocket, _mockParser, mockRouter);
        }

        [Fact]
        public void TestParseIsNotCalledBeforeRunningRequestProcessor() {
            Assert.Equal(_mockParser.GetCallsToParse(), 0);
        }

        [Fact]
        public void TestHandleIsNotCalledBeforeRunningRequestProcessor() {
            Assert.Equal(_mockHandler.GetCallsToHandle(), 0);
        }

        [Fact]
        public void TestSendIsNotCalledBeforeRunningRequestProcessor() {
            Assert.Equal(_mockResponse.GetCallsToSend(), 0);
        }

        [Fact]
        public void TestSocketIsOpenBeforeRunningRequestProcessor() {
            Assert.Equal(_mockSocket.IsClosed(), false);
        }

        [Fact]
        public void TestParseIsCalledAfterRunningRequestProcessor() {
            _requestProcessor.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestParseIsCalledWithStream() {
            _requestProcessor.Run();
            Assert.Equal(_mockParser.GetLastStreamPassedToParse(), _ioStream);
        }

        [Fact]
        public void TestParseReturnsRequest() {
            Assert.Equal(_mockParser.Parse(new StreamReader(_mockSocket.GetStream())), _request);
        }

        [Fact]
        public void TestHandleIsCalledAfterRunningRequestProcessor() {
            _requestProcessor.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestHandleIsCalledWithRequest() {
            _requestProcessor.Run();
            Assert.Equal(_mockHandler.GetLastRequestPassedToHandle(), _request);
        }

        [Fact]
        public void TestHandleReturnsResponse() {
            _requestProcessor.Run();
            Assert.Equal(_mockHandler.Handle(_request), _mockResponse);
        }

        [Fact]
        public void TestSendIsCalledAfterRunningRequestProcessor() {
            _requestProcessor.Run();
            Assert.Equal(_mockResponse.GetCallsToSend(), 1);
        }

        [Fact]
        public void TestSendIsCalledWithStream() {
            _requestProcessor.Run();
            Assert.Equal(_mockResponse.GetLastStreamPassedToSend(), _ioStream);
        }

        [Fact]
        public void TestSocketClosesAfterRunningRequestProcessor() {
            _requestProcessor.Run();
            Assert.Equal(true, _mockSocket.IsClosed());
        }
    }
}