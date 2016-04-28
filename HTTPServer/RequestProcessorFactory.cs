namespace HTTPServer {
    public class RequestProcessorFactory : IServiceFactory {
        private readonly IRouter _router;
        private readonly IParser _parser;


        public RequestProcessorFactory(IParser parser, IRouter router) {
            _parser = parser;
            _router = router;
        }

        public IService CreateService(IClientSocket socket) {
            return new RequestProcessor(socket, _parser, _router);
        }
    }
}