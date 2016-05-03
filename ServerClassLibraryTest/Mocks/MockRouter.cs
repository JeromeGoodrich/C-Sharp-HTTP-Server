using ServerClassLibrary;

namespace ServerClassLibraryTest.Mocks {
    public class MockRouter : IRouter {
        private readonly MockHandler _mockHandler;

        internal MockRouter(MockHandler mockHandler) {
            _mockHandler = mockHandler;
        }

        public IHandler Route(Request request) {
            return _mockHandler;
        }
    }
}