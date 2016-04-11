namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IServiceFactory _serviceFactory;

        public Server(IListener listener, IServiceFactory serviceFactory) {
            this._listener = listener;
            this._serviceFactory = serviceFactory;

        }

        public void Start() {
            while (_listener.Listening()) {
                var socket = _listener.Accept();
                var service = _serviceFactory.CreateService(socket);
                service.Run();
            }
        }

    }
}