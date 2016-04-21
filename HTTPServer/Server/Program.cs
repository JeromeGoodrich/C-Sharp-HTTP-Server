﻿namespace HTTPServer {
    internal class Program {
        private static void Main(string[] args) {
            var config = new ServerConfig(args);
            var listener = new Listener(config.IpAddress, config.Port);
            var parser = new Parser();
            var handler = new RequestHandler(config.Handlers);
            var factory = new ServiceFactory(parser, handler);
            var server = new Server(listener, factory);
            server.Start();
        }
    }
}