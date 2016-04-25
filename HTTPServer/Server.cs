using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IServiceFactory _serviceFactory;

        public Server(IListener listener, IServiceFactory serviceFactory) {
            _listener = listener;
            _serviceFactory = serviceFactory;
        }

        public void Start(CancellationToken token) {
            _listener.Start();
            while (true) {
                
                Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var service = _serviceFactory.CreateService(socket);
                var startTask = Task.Run(() => service.Run());
                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
} 