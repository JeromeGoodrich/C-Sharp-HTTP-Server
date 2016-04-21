using System;
using System.IO;
using System.Text;
using System.Web;

namespace HTTPServer {
    public class FileHandler : IHandler {
        private readonly string _publicDir;

        public FileHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            var file = _publicDir + request.Path;
            var fileBytes = File.ReadAllBytes(file);
            var mimeType = Path.GetExtension(file).Equals("") ? "text/plain" : MimeMapping.GetMimeMapping(file);
            Response response;

            if (request.GetHeaders().ContainsKey("Range")) {
                int rangeStart;
                int rangeEnd;
                var stringRange = request.GetHeader("Range").Split('=')[1];
                if (stringRange.EndsWith("-"))
                {
                    var stringRangeStart = stringRange.Split('-')[0];
                    rangeStart = int.Parse(stringRangeStart);
                    rangeEnd = fileBytes.Length;
                }
                else if (stringRange.StartsWith("-"))
                {
                    rangeEnd = fileBytes.Length;
                    var stringBytesTilEnd = stringRange.Split('-')[1];
                    rangeStart = (fileBytes.Length - int.Parse(stringBytesTilEnd));
                }
                else
                {
                    var stringRangeStart = stringRange.Split('-')[0];
                    var stringRangeEnd = stringRange.Split('-')[1];
                    rangeStart = int.Parse(stringRangeStart);
                    rangeEnd = int.Parse(stringRangeEnd) + 1;
                }
                var range = rangeEnd - rangeStart;
                var data = File.ReadAllText(file);
                var partialData = data.Substring(rangeStart, range);
 
                response = new Response(206, "HTTP/1.1");
                response.Body = Encoding.UTF8.GetBytes(partialData);
            } else {
                response = new Response(200, "HTTP/1.1");
                response.Body = fileBytes;
            }
 
            response.AddHeader("Content-Length", fileBytes.Length.ToString());
            response.AddHeader("Content-Type", mimeType);
      
            if (mimeType.Equals("application/pdf") && fileBytes.Length > 10485760) {
                response.AddHeader("Content-Disposition", "attachment; filename=\"big-pdf.pdf\"");
            }

            
            return response;
        }

        public bool WillHandle(string method, string path) {
            return File.Exists(_publicDir + path);
        }
    }
}