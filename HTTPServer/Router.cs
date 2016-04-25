﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HTTPServer {
    public class Router : IRouter {

        private readonly string _publicDir;
        private readonly Dictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>();

        public Router(string publicDir) {
            _publicDir = publicDir;
            AddHandlers();
        }

        private void AddHandlers() {
            var files = Directory.GetFiles(_publicDir);
            foreach (var file in files) {
                _handlers.Add(file, new FileHandler(_publicDir));
            }
            _handlers.Add("/", new DirHandler(_publicDir));
            _handlers.Add("/logs", new BasicAuthHandler());
        }

        public IHandler Route(Request request) {
            if (_handlers.ContainsKey(request.Path)) {
                return _handlers[request.Path];
            }
            return new NotFoundHandler();
        }
    }
}