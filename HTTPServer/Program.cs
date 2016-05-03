using System.Threading;
using ServerClassLibrary;
using System.IO;

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
            router.AddRoute(new Route("GET", "/logs", new BasicAuthHandler()));
            router.AddRoute(new Route("GET", "/", new DirHandler(config.PublicDir)));
            router.AddRoute(new Route("GET", "/parameters", new ParamsHandler()));
            router.AddRoute(new Route("GET", "/form", new FormDataHandler()));
            router.AddRoute(new Route("POST", "/form", new FormDataHandler()));
            router.AddRoute(new Route("DELETE", "/form", new FormDataHandler()));
            router.AddRoute(new Route("PUT", "/form", new FormDataHandler()));
            router.AddRoute(new Route("GET", "/redirect", new RedirectHandler()));
            router.AddRoute(new Route("GET", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("PATCH", "/patch-content", new FileHandler(config.PublicDir)));
            var files = Directory.GetFiles(config.PublicDir);
            foreach (var file in files)
            {
                var fileIndex = file.Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = "/" + file.Split(Path.DirectorySeparatorChar)[fileIndex];
                router.AddRoute(new Route("GET", fileName, new FileHandler(config.PublicDir)));
            }
                

            var factory = new RequestProcessorFactory(parser, router);
            var server = new Server(listener, factory);
            return server;
        }
    }
}