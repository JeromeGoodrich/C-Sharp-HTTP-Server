using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockRequestProcessorFactory : IServiceFactory {
        private readonly MockRequestProcessor _mockRequestProcessor;

        public MockRequestProcessorFactory(MockRequestProcessor mockRequestProcessor) {
            _mockRequestProcessor = mockRequestProcessor;
        }

        public IService CreateService(IClientSocket socket) {
            _mockRequestProcessor.Socket = socket;
            return _mockRequestProcessor;
        }
    }
}