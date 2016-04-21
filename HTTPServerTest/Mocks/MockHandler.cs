using HTTPServer;

namespace HTTPServerTest.Mocks {
    internal class MockHandler : IHandler {
        private readonly MockResponse _mockResponse;
        private int _callsToHandle;
        private Request _request;

        public MockHandler(MockResponse mockResponse) {
            _mockResponse = mockResponse;
        }

        public IResponse Handle(Request request) {
            _request = request;
            _callsToHandle++;
            return _mockResponse;
        }

        public bool WillHandle(string method, string path) {
            throw new System.NotImplementedException();
        }

        public int GetCallsToHandle() {
            return _callsToHandle;
        }

        public Request GetLastRequestPassedToHandle() {
            return _request;
        }
    }
}