using HTTPServer;

namespace HTTPServerTest
{
    internal class MockServiceFactory : IServiceFactory
    {
        private IService mockService;

        public MockServiceFactory(IService mockService) {
            this.mockService = mockService;
        }

        public IService CreateService(IClientSocket socket) {
            mockService.SetSocket(socket);
            return mockService;
        }

    }
}