using System;
using System.IO;
using System.Text;

namespace HTTPServer {
    public class BasicAuthHandler : IHandler {
        public IResponse Handle(Request request) {
            if (request.GetHeaders().ContainsKey("Authorization")) {
                var encodedCredentials = request.GetHeader("Authorization").Split(' ')[1];
                var accepted = VerifyCredentials(encodedCredentials);
                if (accepted) {
                    return AccessGranted(request);
                }
                return AccessDenied(request);
            }
            return AccessDenied(request);
        }

        public bool WillHandle(string method, string path) {
            throw new NotImplementedException();
        }

        private bool VerifyCredentials(string encodedCredentials) {
            var rawCredentials = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.UTF8.GetString(rawCredentials);
            var username = credentials.Split(':')[0];
            var password = credentials.Split(':')[1];
            return username.Equals("admin") && password.Equals("hunter2");
        }

        private Response AccessGranted(Request request) {
            var response = new Response(200, request.Version);
            using (
                var reader =
                    File.OpenText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HTTPServer\logFile.txt"))) {
                response.Body = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }
            return response;
        }

        private Response AccessDenied(Request request) {
            var response = new Response(401, request.Version);
            response.AddHeader("WWW-Authenticate", "Basic realm=\"Camelot\"");
            return response;
        }
    }
}