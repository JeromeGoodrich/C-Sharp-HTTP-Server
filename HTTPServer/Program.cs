using System.Threading;
using ServerClassLibrary;

namespace CobSpecServer {
    internal class Program {

        private static void Main(string[] args) {
            var config = new CommandLineConfig(args);
            var server = Server(config);
            var tokenSource = new CancellationTokenSource();
            server.Start(tokenSource.Token);
        }

        private static Server Server(CommandLineConfig config) {
            var listener = new Listener(config.IpAddress, config.Port);
            var parser = new Parser();
            var router = new Router(config.PublicDir, config.Logger);
            var factory = new RequestProcessorFactory(parser, router);
            var server = new Server(listener, factory);
            return server;
        }
    }
}