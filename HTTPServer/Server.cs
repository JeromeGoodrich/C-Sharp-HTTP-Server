using System;
using System.IO;
using System.Reflection;
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
                Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var socket = _listener.Accept();
                Console.WriteLine("Accepted Connection.");
                var requestProcessor = _requestProcessorFactor.CreateProcessor(socket);
                var runTask = Task.Run(() => 
                    requestProcessor.Run()
                );

                if (token.IsCancellationRequested) { break; }
            }
        }
    }
} 