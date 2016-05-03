using ServerClassLibrary;

namespace ServerClassLibraryTest.Mocks {
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

        public int GetCallsToHandle() {
            return _callsToHandle;
        }

        public Request GetLastRequestPassedToHandle() {
            return _request;
        }
    }
}