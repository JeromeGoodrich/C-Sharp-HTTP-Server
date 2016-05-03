using ServerClassLibrary;

namespace ServerClassLibraryTest.Mocks {
    public class MockRequestProcessorFactory : IRequestProcessorFactor {
        private readonly MockRequestProcessor _mockRequestProcessor;

        public MockRequestProcessorFactory(MockRequestProcessor mockRequestProcessor) {
            _mockRequestProcessor = mockRequestProcessor;
        }

        public IRequestProcessor CreateProcessor(IClientSocket socket) {
            _mockRequestProcessor.Socket = socket;
            return _mockRequestProcessor;
        }
    }
}