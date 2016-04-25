using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTPServer {
    public class Response : IResponse {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public Response(int statusCode, string version) {
            StatusCode = statusCode;
            Version = version;
        }

        public int StatusCode { get; }
        public string ReasonPhrase => Status.StatusDictionary[StatusCode];
        public string Version { get; }
        public byte[] Body { get; set; }

        public void Send(BinaryWriter writer) {
            var formattedResponse = GetFormattedResponse();
            var bytes = Encoding.UTF8.GetBytes(formattedResponse);
                if (Body != null) {
                var fullResponse = bytes.Concat(Body).ToArray();
                writer.Write(fullResponse);
            }
            else {
                writer.Write(bytes);
            }
            writer.Flush();
        }

        public string GetHeader(string headerName) {
            return _headers[headerName];
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

        public void AddHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }
    }
}