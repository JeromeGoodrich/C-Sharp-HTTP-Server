using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockRouter : IRouter {
        private MockHandler _mockHandler;

        internal MockRouter(MockHandler mockHandler) {
            _mockHandler = mockHandler;
        }

        public IHandler Route(Request request) {
            return _mockHandler;
        }
    }
}