namespace HTTPServer {
    public class RedirectHandler : IHandler {
        public IResponse Handle(Request request) {
            var response = new Response(302, request.Version);
            response.AddHeader("Location", "http://localhost:5000/");
            return response;
        }
    }
}