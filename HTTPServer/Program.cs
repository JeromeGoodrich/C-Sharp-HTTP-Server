using System.Threading;

namespace HTTPServer {
    internal class Program {

        private static void Main(string[] args) {
            var config = new ServerConfig(args);
            var server = Server(config);
            var tokenSource = new CancellationTokenSource();
            server.Start(tokenSource.Token);
        }

        private static Server Server(ServerConfig config) {
            var listener = new Listener(config.IpAddress, config.Port);
            var parser = new Parser();
            var router = new Router(config.PublicDir);
            var factory = new ServiceFactory(parser, router);
            var server = new Server(listener, factory);
            return server;
        }
    }
}