using System;

namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IServiceFactory _serviceFactory;

        public Server(IListener listener, IServiceFactory serviceFactory) {
            _listener = listener;
            _serviceFactory = serviceFactory;
        }

        public void Start() {
            _listener.Start();
            //cancellation token for a while loop - part of the Task Library
            while (_listener.Listening()) {
                Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var service = _serviceFactory.CreateService(socket);
                service.Run();
            }
        }
    }
}