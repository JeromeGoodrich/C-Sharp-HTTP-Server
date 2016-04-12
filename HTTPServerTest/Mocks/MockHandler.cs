using HTTPServer;

namespace HTTPServerTest.Mocks {

    internal class MockHandler : IHandler {
        private readonly MockResponse _mockResponse;
        private int _callsToHandle;

        public MockHandler(MockResponse mockResponse) { 
            _mockResponse = mockResponse;
        }

        public int GetCallsToHandle() {
            return _callsToHandle;
        }
    
        public IResponse Handle(Request request) {
            _callsToHandle++;
            return _mockResponse;
        }
    }
}