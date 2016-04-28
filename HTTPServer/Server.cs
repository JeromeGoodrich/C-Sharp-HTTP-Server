using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IRequestProcessorFactor _serviceFactory;

        public Server(IListener listener, IRequestProcessorFactor serviceFactory) {
            _listener = listener;
            _serviceFactory = serviceFactory;
        }

        public void Start(CancellationToken token) {
            _listener.Start();
            while (true) {
                
                Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var service = _serviceFactory.CreateProcessor(socket);
                var runTask = Task.Run(() => service.Run());
                if (token.IsCancellationRequested) { break; }
            }
        }
    }
} 