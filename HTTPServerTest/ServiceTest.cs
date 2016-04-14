using Xunit;
using HTTPServer;
using System.IO;
using HTTPServerTest.Mocks;

namespace HTTPServerTest {

    public class ServiceTest {
        private MemoryStream _ioStream;
        private MockSocket _mockSocket;
        private Request _request;
        private MockParser _mockParser;
        private MockResponse _mockResponse;
        private MockHandler _mockHandler;
        private Service _service;


        private void TestSetUp() {
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
            TestSetUp();
            Assert.Equal(_mockParser.GetCallsToParse(), 0);
        }

        [Fact]
        public void TestHandleIsNotCalledBeforeRunningService() {
            TestSetUp();
            Assert.Equal(_mockHandler.GetCallsToHandle(), 0);
        }

        [Fact]
        public void TestSendIsNotCalledBeforeRunningService() {
            TestSetUp();
            Assert.Equal(_mockResponse.GetCallsToSend(), 0);
        }

        [Fact]
        public void TestSocketIsOpenBeforeRunningService() {
            TestSetUp();
            Assert.Equal(_mockSocket.IsClosed(), false);
        }

        [Fact]
        public void TestParseIsCalledAfterRunningService() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestParseIsCalledWithStream() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockParser.GetLastStreamPassedToParse(), _ioStream);
        }

        [Fact]
        public void TestParseReturnsRequest() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockParser.Parse(_mockSocket.GetStream()), _request);
        }

        [Fact]
        public void TestHandleIsCalledAfterRunningService() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockParser.GetCallsToParse(), 1);
        }

        [Fact]
        public void TestHandleIsCalledWithRequest() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockHandler.GetLastRequestPassedToHandle(), _request);
        }

        [Fact]
        public void TestHandleReturnsResponse() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockHandler.Handle(_request), _mockResponse);
        }

        [Fact]
        public void TestSendIsCalledAfterRunningService() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockResponse.GetCallsToSend(), 1);
        }

        [Fact]
        public void TestSendIsCalledWithStream() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockResponse.GetLastStreamPassedToSend(), _ioStream);
        }

        [Fact]
        public void TestSocketClosesAfterRunningService() {
            TestSetUp();
            _service.Run();
            Assert.Equal(_mockSocket.IsClosed(), true);
        }
    }
}