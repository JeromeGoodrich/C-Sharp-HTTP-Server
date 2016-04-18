using System;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTPServer {
    public class Parser : IParser {
        public Request Parse(StreamReader reader) {
            var request = new Request();
            var rawRequest = reader.ReadLine();
            var requestLineAndHeaders = SplitBodyfromRest(rawRequest, request);
            SplitRequestLineAndHeaders(requestLineAndHeaders, request);
            return request;
            
        } 

        private void SplitRequestLineAndHeaders(string requestLineAndHeaders, Request request) {
            var splitRequestLineAndHeaders = requestLineAndHeaders.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var requestLine = splitRequestLineAndHeaders[0];
            var headers = splitRequestLineAndHeaders.Skip(1).ToArray();
            ParseRequestLine(requestLine, request);
            ParseHeaders(headers, request);
        }

        private string SplitBodyfromRest(string rawRequest, Request request) {
            var splitRawRequest = rawRequest.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            var requestLineAndHeaders = splitRawRequest[0];
            if (splitRawRequest.Length > 1) {
                var body = splitRawRequest[1];
                ParseBody(body, request);
            }
            return requestLineAndHeaders;
        }

        private void ParseBody(string body, Request request) {
            request.Body = body;
        }

        private void ParseHeaders(string[] headers, Request request) {
            foreach (var header in headers) {
                var splitHeader = header.Split(':');
                var headerName = splitHeader[0];
                var headerValue = splitHeader[1].TrimStart(' ');
                request.AddHeader(headerName, headerValue);
            }
            
        }

        private void ParseRequestLine(string requestLine, Request request) {
            var splitRequestLine = requestLine.Split(' ');
            System.Console.WriteLine("Hello Tom:" + requestLine);
            var method = splitRequestLine[0];
            var path = splitRequestLine[1];
            var version = splitRequestLine[2];
            request.Method = method;
            request.Path = path;
            request.Version = version;
        }
    }
}