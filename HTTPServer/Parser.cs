using System;
using System.IO;
using System.Linq;

namespace HTTPServer {
    public class Parser : IParser {
        public Request Parse(Stream stream) {
            var reader = new StreamReader(stream);
            var rawRequest = reader.ReadToEnd();
            var splitRawRequest = rawRequest.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);
            var requestLineAndHeaders = splitRawRequest[0];
            if (splitRawRequest.Length > 1) {
                var body = splitRawRequest[1];
                ParseBody(body);
            }
            var splitRequestLineAndHeaders = requestLineAndHeaders.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var requestLine = splitRequestLineAndHeaders[0];
            var headers = splitRequestLineAndHeaders.Skip(1).ToArray();
            ParseRequestLine(requestLine);
            ParseHeaders(headers);
            return new Request();
        }

        private void ParseBody(string body) {
            
        }

        private void ParseHeaders(string[] headers) {
            
        }

        private void ParseRequestLine(string requestLine) {
            
        }
    }
}