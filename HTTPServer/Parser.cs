using System;
using System.IO;
using System.Linq;

namespace HTTPServer {
    public class Parser : IParser {
        public Request Parse(Stream stream) {
            var request = new Request();
            var reader = new StreamReader(stream);
            var rawRequest = reader.ReadToEnd();
            var splitRawRequest = rawRequest.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);
            var requestLineAndHeaders = splitRawRequest[0];
            if (splitRawRequest.Length > 1) {
                var body = splitRawRequest[1];
                ParseBody(body, request);
            }
            var splitRequestLineAndHeaders = requestLineAndHeaders.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var requestLine = splitRequestLineAndHeaders[0];
            var headers = splitRequestLineAndHeaders.Skip(1).ToArray();
            ParseRequestLine(requestLine, request);
            ParseHeaders(headers, request);
            return request;
        }

        private void ParseBody(string body, Request request) {
            request.SetBody(body);
        }

        private void ParseHeaders(string[] headers, Request request) {
            foreach (var header in headers) {
                var splitHeader = header.Split(':');
                var headerName = splitHeader[0];
                var headerValue = splitHeader[1].TrimStart(' ');
                request.SetHeader(headerName, headerValue);
            }
            
        }

        private void ParseRequestLine(string requestLine, Request request) {
            var splitRequestLine = requestLine.Split(' ');
            var method = splitRequestLine[0];
            var path = splitRequestLine[1];
            var version = splitRequestLine[2];
            request.SetMethod(method);
            request.SetPath(path);
            request.SetVersion(version);
        }
    }
}