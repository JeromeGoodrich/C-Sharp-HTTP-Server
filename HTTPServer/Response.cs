using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HTTPServer {
    public class Response : IResponse {

        public int StatusCode { get; }
        public string ReasonPhrase => Status.StatusDictionary[StatusCode];
        public string Version { get; }
        public byte[] Body { get; set; }
        private readonly Dictionary<string,string> _headers = new Dictionary<string, string>();

        public Response(int statusCode, string version) {
            StatusCode = statusCode;
            Version = version;
        }

        public void Send(Stream ioStream) {
            var formattedResponse = GetFormattedResponse();
            var bytes = Encoding.UTF8.GetBytes(formattedResponse);
            var length = bytes.Length;
            ioStream.Write(bytes, 0, length);
            if (Body != null) {
                ioStream.Write(Body, 0, Body.Length);
            }
        }

        private string GetFormattedResponse() {
            var statusLine = Version + " " + StatusCode + " " + ReasonPhrase + "\r\n";
            var headers = "";
            foreach (var header in _headers) {
                var headerName = header.Key;
                var headerValue = header.Value;
                headers += headerName + ":" + " " + headerValue + "\r\n";
            }
            var formattedResponse = statusLine + headers + "\r\n";
            return formattedResponse;
        }

        public string GetHeader(string headerName) {
            return _headers[headerName];
        }

        public void AddHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }
    }
}