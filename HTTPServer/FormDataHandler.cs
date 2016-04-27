namespace HTTPServer {
    public class FormDataHandler : IHandler {
        public IResponse Handle(Request request) {
            return new Response(200, request.Version);
        }
    }
}