using System;
using System.IO;
using System.Linq;

namespace HTTPServer {
    public class Parser : IParser {
        private readonly FileLogger _logger = new FileLogger();

        public Request Parse(StreamReader reader) {
            var request = new Request();
            string line;
            var requestLineAndHeaders = "";
            while ((line = reader.ReadLine()) != "") {
                requestLineAndHeaders += line + "\r\n";
            }
            SplitRequestLineAndHeaders(requestLineAndHeaders, request);
            if (request.GetHeaders().Keys.Contains("Content-Length")) {
                var contentLength = int.Parse(request.GetHeader("Content-Length"));
                var rawBody = new char[contentLength];

                reader.Read(rawBody, 0, contentLength);
                var body = new string(rawBody);
                request.Body = body;
            }
            return request;
        }

        private void SplitRequestLineAndHeaders(string requestLineAndHeaders, Request request) {
            var splitRequestLineAndHeaders = requestLineAndHeaders.Split(new[] {"\r\n"}, StringSplitOptions.None);
            var requestLine = splitRequestLineAndHeaders[0];
            var headers = splitRequestLineAndHeaders.Skip(1).ToArray();
            ParseRequestLine(requestLine, request);
            ParseHeaders(headers, request);
        }


        private void ParseBody(Request request) {}

        private void ParseHeaders(string[] headers, Request request) {
            foreach (var header in headers) {
                if (header != "") {
                    var splitHeader = header.Split(':');
                    var headerName = splitHeader[0];
                    var headerValue = splitHeader[1].TrimStart(' ');
                    request.AddHeader(headerName, headerValue);
                }
            }
        }

        private void ParseRequestLine(string requestLine, Request request) {
            var splitRequestLine = requestLine.Split(' ');
            var method = splitRequestLine[0];
            var path = splitRequestLine[1];
            var version = splitRequestLine[2];
            request.Method = method;
            request.Path = path;
            request.Version = version;
            _logger.Log(requestLine);
        }
    }
}