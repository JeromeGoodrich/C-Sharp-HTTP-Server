using System.IO;

namespace HTTPServer {
    public class FileHandler : IHandler {
        private readonly string _publicDir;

        public FileHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            var response = new Response(200, "HTTP/1.1");
            var fileBytes = File.ReadAllBytes(_publicDir + request.Path);
            response.AddHeader("Content-Length", fileBytes.Length.ToString());
            response.AddHeader("Content-Type", "text/plain");
            response.Body = fileBytes;
            return response;
        }

        public bool WillHandle(string method, string path) {
            return File.Exists(_publicDir + path);
        }
    }
}