using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockServiceFactory : IServiceFactory {
        private readonly MockService _mockService;

        public MockServiceFactory(MockService mockService) {
            _mockService = mockService;
        }

        public IService CreateService(IClientSocket socket) {
            _mockService.Socket= socket;
            return _mockService;
        }

    }
}