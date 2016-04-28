using System;
using System.Collections.Generic;
using System.IO;

namespace HTTPServer {
    public class Router : IRouter {

        private readonly string _publicDir;
        private readonly Dictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>();

        public Router(string publicDir, FileLogger logger) {
            _publicDir = publicDir;
            AddHandlers();
        }

        private void AddHandlers() {
            var files = Directory.GetFiles(_publicDir);
            foreach (var file in files) {
                var fileIndex = file.Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = "/" + file.Split(Path.DirectorySeparatorChar)[fileIndex];
                _handlers.Add(fileName, new FileHandler(_publicDir));
            }
            _handlers.Add("/", new DirHandler(_publicDir));
            _handlers.Add("/logs", new BasicAuthHandler());
            _handlers.Add("/parameters", new ParamsHandler());
            _handlers.Add("/form", new FormDataHandler());
            _handlers.Add("/redirect", new RedirectHandler());
            _handlers.Add("/method_options", new OptionsHandler());
        }

        public IHandler Route(Request request) {

            if (_handlers.ContainsKey(request.Path)) {
                return _handlers[request.Path];
            }
            return new NotFoundHandler();
        }
    }
}