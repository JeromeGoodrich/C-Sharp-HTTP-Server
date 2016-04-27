using System;
using System.IO;
using System.Text;
using System.Web;

namespace HTTPServer {
    public class FileHandler : IHandler {
        private readonly string _publicDir;
        private string _file;
        private int _fileSize;
        private string _mimeType;
        private string _version;

        public FileHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            _file = _publicDir + @"\" + request.Path.TrimStart('/');
            _fileSize = File.ReadAllBytes(_file).Length;
            _mimeType = Path.GetExtension(_file).Equals("") ? "text/plain" : MimeMapping.GetMimeMapping(_file);
            _version = request.Version;
            if (request.Method.Equals("GET")) {
                return request.GetHeaders().ContainsKey("Range") ? HandlePartialContent(request) : HandleFile();
            }
            if (request.Method.Equals("PATCH") && request.Path.Equals("/patch-content.txt")) {
                return HandlePatchRequest(request);
            }
            return new Response(405, _version);
        }

        private IResponse HandlePatchRequest(Request request) {
            var response = new Response(204, _version);
            var patchedContent = request.Body;
            using (var writer = new StreamWriter(_file))
            {
                writer.Write(patchedContent);
            }
            response.AddHeader("ETag", request.GetHeader("If-Match"));
            return response;
        }

        private IResponse HandlePartialContent(Request request) {
            int rangeStart;
            int rangeEnd;
            var rawRange = request.GetHeader("Range").Split('=')[1];
            
            if (rawRange.EndsWith("-")) {
                var rawRangeStart = rawRange.Split('-')[0];
                rangeStart = int.Parse(rawRangeStart);
                rangeEnd = _fileSize-1;
            } else if (rawRange.StartsWith("-")) {
                rangeEnd = _fileSize-1;
                var stringBytesTilEnd = rawRange.Split('-')[1];
                rangeStart = rangeEnd - int.Parse(stringBytesTilEnd);
            } else {
                var stringRangeStart = rawRange.Split('-')[0];
                var stringRangeEnd = rawRange.Split('-')[1];
                rangeStart = int.Parse(stringRangeStart);
                rangeEnd = int.Parse(stringRangeEnd)+1;
            }

            
            var range = rangeEnd - rangeStart;
            Console.WriteLine("Start: " + rangeStart + " End: " + rangeEnd + " Range: " + range + "FileSize: " + _fileSize);
            var data = File.ReadAllBytes(_file);
            var partialData = new byte[range];
            Array.Copy(data, rangeStart, partialData, 0, range);

            var response = new Response(206, _version);
            response.AddHeader("Content-Length", range.ToString());
            response.AddHeader("Content-Type", _mimeType);
            response.Body = partialData;

            return response;
        }

        private IResponse HandleFile() {
            var response = new Response(200, _version);
            response.AddHeader("Content-Length", _fileSize.ToString());
            response.AddHeader("Content-Type", _mimeType);

            if (_mimeType.Equals("application/pdf") && _fileSize > 10485760) {
                response.AddHeader("Content-Disposition", "attachment; filename=\"big-pdf.pdf\"");
            }

            response.Body = File.ReadAllBytes(_file);
            return response;
        }
    }
}