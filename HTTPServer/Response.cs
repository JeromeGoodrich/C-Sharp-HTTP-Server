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
            var statusLine = Version + " " + StatusCode + " " + ReasonPhrase + "\r\n";
            var headers = "";
            foreach (var pair in _headers) {
                var key = pair.Key;
                var value = pair.Value;
                headers += key + ":" + " " + value + "\r\n";
            }
            var formattedResponse = statusLine + headers + "\r\n";
            var bytes = Encoding.UTF8.GetBytes(formattedResponse);
            var length = bytes.Length;
            ioStream.Write(bytes, 0, length);
            if (Body != null) {
                ioStream.Write(Body, 0, Body.Length);
            }
        }

        public string GetHeader(string headerName) {
            return _headers[headerName];
        }

        public void AddHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }
    }
}