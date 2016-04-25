namespace HTTPServer {
    public class ServiceFactory : IServiceFactory {
        private readonly IRouter _router;
        private readonly IParser _parser;


        public ServiceFactory(IParser parser, IRouter router) {
            _parser = parser;
            _router = router;
        }

        public IService CreateService(IClientSocket socket) {
            return new Service(socket, _parser, _router);
        }
    }
}