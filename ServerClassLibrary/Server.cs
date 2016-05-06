using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClassLibrary {
    public class Server {
        private readonly IListener _listener;
        private readonly IRequestProcessorFactor _requestProcessorFactor;
        private readonly FileLogger _logger;

        public Server(IListener listener, IRequestProcessorFactor requestProcessorFactor, FileLogger logger) {
            _listener = listener;
            _requestProcessorFactor = requestProcessorFactor;
            _logger = logger;
        }

        public void Start(CancellationToken token) {
            _listener.Start();
            while (true) {
                
                _logger.Log("Waiting...");
                var socket = _listener.Accept();
                _logger.Log("Accepted Connection.");
                var requestProcessor = _requestProcessorFactor.CreateProcessor(socket);
                var runTask = Task.Run(() => 
                    requestProcessor.Run()
                );

                if (token.IsCancellationRequested) { break; }
            }
        }
    }
} 