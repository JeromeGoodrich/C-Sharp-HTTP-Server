using System;
using Xunit;
using HTTPServer;

namespace HTTPServerTest {

    public class ServiceTest {
        [Fact]
        public void TestRunService() {
            IHandler mockHandler = new MockHandler();
            MockSocket mockSocket = new MockSocket();
            var service = new Service(mockSocket, mockHandler);
            Assert.Equal(mockHandler.GetCallsToHandle(), 0);
            Assert.Equal(mockSocket.IsClosed(), false);
            service.Run();
            Assert.Equal(mockHandler.GetCallsToHandle(), 1);
            Assert.Equal(mockSocket.IsClosed(), true);
        }
    }
}