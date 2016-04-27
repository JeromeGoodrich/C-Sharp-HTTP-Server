namespace HTTPServer {
    public class OptionsHandler : IHandler {
        public IResponse Handle(Request request) {
            var response = new Response(200, request.Version);
            return response;
        }
    }
}