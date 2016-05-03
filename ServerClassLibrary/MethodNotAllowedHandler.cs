using System;

namespace ServerClassLibrary {
    public class MethodNotAllowedHandler : IHandler {
        public IResponse Handle(Request request) {
            return new Response(405, request.Version);
        }
    }
}
