using System;
using System.Threading;

namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IServiceFactory _serviceFactory;

        public Server(IListener listener, IServiceFactory serviceFactory) {
            _listener = listener;
            _serviceFactory = serviceFactory;
        }

        public void Start(CancellationToken token) {
           // _listener.Start();
            while (_listener.Listening()) {
                Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var service = _serviceFactory.CreateService(socket);
                service.Run();

                if (token.IsCancellationRequested) {
                    token.ThrowIfCancellationRequested();
                }

            }
        }
    }
}