using System;
using System.Text;

namespace HTTPServer {
    public class BasicAuthHandler {
        public IResponse Handle(Request request) {
            Response response;
            if (request.GetHeaders().ContainsKey("Authorization")) {
                var encodedCredentials = request.GetHeader("Authorization").Split(' ')[1];
                var rawCredentials = Convert.FromBase64String(encodedCredentials);
                var credentials = Encoding.UTF8.GetString(rawCredentials);
                var username = credentials.Split(':')[0];
                var password = credentials.Split(':')[1];
                if (username.Equals("admin") && password.Equals("hunter2")) {
                    response = new Response(200, request.Version);
                }
                else {
                    response = new Response(401, request.Version);
                    response.AddHeader("WWW-Authenticate", "Basic realm=\"Camelot\"");
                }
            }
            else {
                response = new Response(401, request.Version);
                response.AddHeader("WWW-Authenticate", "Basic realm=\"Camelot\"");
            }
            return response;
        }
    }
}