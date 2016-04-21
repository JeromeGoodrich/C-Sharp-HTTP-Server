using System.IO;
using System.Web;

namespace HTTPServer {
    public class FileHandler : IHandler {
        private readonly string _publicDir;

        public FileHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            var response = new Response(200, "HTTP/1.1");
            var filePath = _publicDir + request.Path;
            var extension = Path.GetExtension(filePath);
            string mimeType;
            if (!extension.Equals("")) {
                mimeType = MimeMapping.GetMimeMapping(filePath);
            } else {
                mimeType = "text/plain";
            }
            var fileBytes = File.ReadAllBytes(_publicDir + request.Path);
            response.AddHeader("Content-Length", fileBytes.Length.ToString());
            response.AddHeader("Content-Type", mimeType);
            if (mimeType.Equals("application/pdf") && fileBytes.Length > 10485760) {
                response.AddHeader("Content-Disposition", "attachment; filename=\"big-pdf.pdf\"");
            }
            response.Body = fileBytes;
            return response;
        }

        public bool WillHandle(string method, string path) {
            return File.Exists(_publicDir + path);
        }
    }
}