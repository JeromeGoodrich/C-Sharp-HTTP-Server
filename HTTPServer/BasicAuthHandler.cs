namespace HTTPServer {
    public class BasicAuthHandler {
        public IResponse Handle(Request request) {
            Response response;
            if (request.GetHeaders().ContainsKey("Authorization")) {
                response = new Response(200, request.Version);
            } else {
                response = new Response(401, request.Version);
                response.AddHeader("WWW-Authenticate", "Basic realm=\"Camelot\"");
            }
            return response;
        } 
    }
}