namespace HTTPServer {
    public class BasicAuthHandler {
        public IResponse Handle(Request request) {
            var response = new Response(401, request.Version);
            return response;
        } 
    }
}