using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer {
    public class Server {
        private readonly IListener _listener;
        private readonly IRequestProcessorFactor _requestProcessorFactor;

        public Server(IListener listener, IRequestProcessorFactor requestProcessorFactor) {
            _listener = listener;
            _requestProcessorFactor = requestProcessorFactor;
        }

        public void Start(CancellationToken token) {
            _listener.Start();
            while (true) {
                
                Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var requestProcessor = _requestProcessorFactor.CreateProcessor(socket);
                var runTask = Task.Run(() => requestProcessor.Run());

                if (token.IsCancellationRequested) { break; }
            }
        }
    }
} 