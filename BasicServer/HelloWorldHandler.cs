using System;
using ServerClassLibrary;
using System.Text;

namespace BasicServer {
    public class HelloWorldHandler : IHandler {
        public IResponse Handle(Request request) {
            var response = new Response(200, request.Version) {
                Body = Encoding.UTF8.GetBytes("Hello World")
            };
            return response;
        }
    }
}
