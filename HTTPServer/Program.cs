using HTTPServer;
namespace HTTPServer {
    class Program {
        static void Main(string[] args) {
            var config = new ServerConfig(args);
            var listener = new Listener(config.GetIpAddress(), config.GetPort());
            var parser = new Parser();
            var handler = new DirHandler(config.GetPublicDir());
            var factory = new ServiceFactory(parser, handler);
            var server = new Server(listener, factory);
            server.Start();
        }
    }
}
