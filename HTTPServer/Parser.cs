using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HTTPServer {
    public class Parser : IParser {

        public Request Parse(StreamReader reader) {
            var request = new Request();
            string line;
            var requestLineAndHeaders = "";
            while ((line = reader.ReadLine()) != "") {
                requestLineAndHeaders += line + "\r\n";
            }
            Debug.WriteLine(requestLineAndHeaders);
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
            String[] rawVariables = parameters.Split('&');
            foreach (var variable in rawVariables) {
                string[] nameAndRawValue = variable.Split('=');
                var variableName = nameAndRawValue[0];
                var rawVariableValue = nameAndRawValue[1];
                string variableValue = decodeRawValue(rawVariableValue);
                request.AddParameters(variableName, variableValue);
            }
        }

        private Dictionary<string, string> URLDecoding = new Dictionary<string, string>() {
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

        private string decodeRawValue(string rawValue) {
            string decodedValue = rawValue;
            foreach (var value in URLDecoding)
            {
                string urlCode = value.Key;
                string decodedValues = value.Value;
                decodedValue = decodedValue.Replace(urlCode, decodedValues);
            }
            return decodedValue;
        }
    }
}