using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer {
    internal class Program {

        private static void Main(string[] args) {
            var config = new ServerConfig(args);
            var server = Server(config);
            var tokenSource = new CancellationTokenSource();
            var startTask = Task.Run(() => server.Start(tokenSource.Token));
            startTask.Wait();
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