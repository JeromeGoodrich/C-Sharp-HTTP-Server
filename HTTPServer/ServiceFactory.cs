namespace HTTPServer {
    public class ServiceFactory : IServiceFactory {
        private readonly IParser _parser;
        private readonly IHandler _handler;

        
        public ServiceFactory(IParser parser, IHandler handler) {
            _parser = parser;
            _handler = handler;
        }

        public IService CreateService(IClientSocket socket) {
            return new Service(socket, _parser, _handler);
        }
    }
}
