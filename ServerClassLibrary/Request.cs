using System.Collections.Generic;

namespace ServerClassLibrary {
    public class Request {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();
        public string Method { get; set; }
        public string Path { get; set; }
        public string Version { get; set; }
        public string Body { get; set; }

        public string GetHeader(string headerName) {
            return _headers[headerName];
        }

        public void AddHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }

        public Dictionary<string, string> GetHeaders() {
            return _headers;
        }

        public void AddParameters(string key, string value) {
            _parameters.Add(key, value);
        }

        public Dictionary<string, string> GetParameters() {
            return _parameters;
        }
    }
}