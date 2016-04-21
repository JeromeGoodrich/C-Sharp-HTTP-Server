namespace HTTPServer {
    public class NotFoundHandler : IHandler {

        public IResponse Handle(Request request) {
            return new Response(404, "HTTP/1.1");
        }

        public bool WillHandle(string method, string path) {
            return true;
        }
    }
}