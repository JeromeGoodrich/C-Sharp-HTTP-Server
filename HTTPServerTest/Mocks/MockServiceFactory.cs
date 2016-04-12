using HTTPServer;

namespace HTTPServerTest.Mocks {
    internal class MockServiceFactory : IServiceFactory {
        private readonly MockService _mockService;

        public MockServiceFactory(MockService mockService) {
            _mockService = mockService;
        }

        public IService CreateService(IClientSocket socket) {
            _mockService.SetSocket(socket);
            return _mockService;
        }

    }
}