using System.Collections.Generic;
using System.Linq;

namespace HTTPServer.Router {
    public class Router {
        private readonly List<IHandler> _handlers;

        public Router(List<IHandler> handlers) {
            _handlers = handlers;
        }

        public IResponse Route(Request request) {
            IResponse response = null;
            foreach (var handler  in _handlers) {
                if (handler.WillHandle(request.Method, request.Path)) {
                    response = handler.Handle(request);
                    break;
                }
            }
            return response;
        }
    }
}