namespace HTTPServer {
    public class DirHandler : IHandler {
        public IResponse Handle(Request request) {
            var response = new Response(200, request.GetVersion());
            return response;
        }
    }
}