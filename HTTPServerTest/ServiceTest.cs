using Xunit;
using HTTPServer;
using System.IO;
using HTTPServerTest.Mocks;

namespace HTTPServerTest {

    public class ServiceTest {
        [Fact]
        public void TestRunService() {
            var ioStream = new MemoryStream(new byte[1024]);
            var mockSocket = new MockSocket(ioStream);
            var request = new Request();
            var mockParser = new MockParser(request);
            var mockResponse = new MockResponse();
            var mockHandler = new MockHandler(mockResponse);
            var service = new Service(mockSocket, mockParser, mockHandler);

            Assert.Equal(mockHandler.GetCallsToHandle(), 0);
            Assert.Equal(mockParser.GetCallsToParse(), 0);
            Assert.Equal(mockResponse.GetCallsToSend(), 0);
            Assert.Equal(mockSocket.IsClosed(), false);

            service.Run();

            Assert.Equal(mockParser.GetCallsToParse(), 1);
            Assert.Equal(mockParser.GetLastStreamPassedToParse(), ioStream);
            Assert.Equal(mockParser.Parse(mockSocket.GetStream()), request);

            Assert.Equal(mockHandler.GetCallsToHandle(), 1);
            Assert.Equal(mockHandler.GetLastRequestPassedToHandle(), request);
            Assert.Equal(mockHandler.Handle(request), mockResponse);

            Assert.Equal(mockResponse.GetCallsToSend(), 1);
            Assert.Equal(mockResponse.GetLastStreamPassedToSend(), ioStream);

            Assert.Equal(mockSocket.IsClosed(), true);
        }
    }
}