using System.Collections.Generic;
using System.Linq;

namespace HTTPServer {
    public class RequestHandler : IHandler {
        private readonly List<IHandler> _handlers;

        public RequestHandler(List<IHandler> handlers) {
            _handlers = handlers;
        }

        public IResponse Handle(Request request) {
            IResponse response = null;
            foreach (var handler  in _handlers) {
                if (handler.WillHandle(request.Method, request.Path)) {
                    response = handler.Handle(request);
                    break;
                }
            }
            return response;
        }

        public bool WillHandle(string method, string path) {
            throw new System.NotImplementedException();
        }
    }
}