using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HTTPServer {
    public class BasicAuthHandler : IHandler {

        private readonly string _logFile = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\Logs\LogFile.txt");
        public IResponse Handle(Request request) {
            if (!request.GetHeaders().ContainsKey("Authorization")) return RequestAuth(request);
            var encodedCredentials = request.GetHeader("Authorization").Split(' ')[1];
            var accepted = VerifyCredentials(encodedCredentials);
            return accepted ? GrantAccess(request) : RequestAuth(request);
        }

        private bool VerifyCredentials(string encodedCredentials) {
            var rawCredentials = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.UTF8.GetString(rawCredentials);
            var username = credentials.Split(':')[0];
            var password = credentials.Split(':')[1];
            return username.Equals("admin") && password.Equals("hunter2");
        }

        private Response GrantAccess(Request request) {
            var response = new Response(200, request.Version);
            if (!File.Exists(_logFile)) {
                new FileStream(_logFile, FileMode.CreateNew).Dispose();
            }
            using (var reader = File.OpenText(_logFile)) {
                response.Body = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }
            return response;
        }

        private Response RequestAuth(Request request) {
            var response = new Response(401, request.Version);
            response.AddHeader("WWW-Authenticate", "Basic realm=\"Camelot\"");
            return response;
        }
    }
}