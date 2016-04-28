using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HTTPServer {
    public class Parser : IParser {

        public Request Parse(StreamReader reader) {
            var request = new Request();
            var requestLineAndHeaders = GetRequestLineAndHeaders(reader);
            SplitRequestLineAndHeaders(requestLineAndHeaders, request);
            if (!request.GetHeaders().Keys.Contains("Content-Length")) return request;
            ParseBody(reader, request);
            return request;
        }

        private string GetRequestLineAndHeaders(StreamReader reader) {
            string line;
            var requestLineAndHeaders = "";
            while ((line = reader.ReadLine()) != "") {
                requestLineAndHeaders += line + "\r\n";
            }
            return requestLineAndHeaders;
        }

        private void SplitRequestLineAndHeaders(string requestLineAndHeaders, Request request) {
            var splitRequestLineAndHeaders = requestLineAndHeaders.Split(new[] {"\r\n"}, StringSplitOptions.None);
            var requestLine = splitRequestLineAndHeaders[0];
            var headers = splitRequestLineAndHeaders.Skip(1).ToArray();
            ParseRequestLine(requestLine, request);
            ParseHeaders(headers, request);
        }


        private void ParseBody(StreamReader reader, Request request) {
            var contentLength = int.Parse(request.GetHeader("Content-Length"));
            var rawBody = new char[contentLength];

            reader.Read(rawBody, 0, contentLength);
            var body = new string(rawBody);
            request.Body = body;
        }

        private void ParseHeaders(string[] headers, Request request) {
            foreach (var header in headers) {
                if (header == "") continue;
                var splitHeader = header.Split(':');
                var headerName = splitHeader[0];
                var headerValue = splitHeader[1].TrimStart(' ');
                request.AddHeader(headerName, headerValue);
            }
        }

        private void ParseRequestLine(string requestLine, Request request) {
            var splitRequestLine = requestLine.Split(' ');
            var method = splitRequestLine[0];
            var path = splitRequestLine[1];
            if (path.Contains("?")) {
                var parameters = path.Split('?')[1];
                path = path.Split('?')[0];
                ParseParams(parameters, request);
            }
            
            var version = splitRequestLine[2];
            request.Method = method;
            request.Path = path;
            request.Version = version;
        }

        private void ParseParams(string parameters, Request request) {
            var rawVariables = parameters.Split('&');
            foreach (var variable in rawVariables) {
                var nameAndRawValue = variable.Split('=');
                var variableName = nameAndRawValue[0];
                var rawVariableValue = nameAndRawValue[1];
                var variableValue = DecodeRawValue(rawVariableValue);
                request.AddParameters(variableName, variableValue);
            }
        }

        private readonly Dictionary<string, string> _urlDecoding = new Dictionary<string, string>() {
            {"20", " "},
            {"22", "\""},
            {"23", "#"},
            {"24", "$"},
            {"26", "&"},
            {"40", "@"},
            {"43", "+"},
            {"2B", "+"},
            {"2C", ","},
            {"2F", "/"},
            {"3A", ":"},
            {"3B", ";"},
            {"3C", "<"},
            {"3D", "="},
            {"3E", ">"},
            {"3F", "?"},
            {"5B", "["},
            {"5D", "]"},
            {"%", ""}
        };

        private string DecodeRawValue(string rawValue) {
            var decodedValue = rawValue;
            foreach (var value in _urlDecoding) {
                var urlCode = value.Key;
                var decodedValues = value.Value;
                decodedValue = decodedValue.Replace(urlCode, decodedValues);
            }
            return decodedValue;
        }
    }
}