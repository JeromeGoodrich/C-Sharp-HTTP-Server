using System.Collections.Generic;

namespace HTTPServer {
    public class Request {
        private string _method;
        private string _path;
        private string _version;
        private Dictionary<string,string> _headers = new Dictionary<string, string>();
        private string _body;

        public string GetMethod() {
            return _method;
        }

        public string GetPath()
        {
            return _path;
        }

        public string GetVersion()
        {
            return _version;
        }

        public string GetHeader(string headerName) {
            return _headers[headerName];
        }

        public string GetBody()
        {
            return _body;
        }

        public void SetMethod(string method) {
            _method = method;
        }

        public void SetPath(string path) {
            _path = path;
        }

        public void SetVersion(string version) {
            _version = version;
        }

        public void SetHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }

        public void SetBody(string body) {
            _body = body;
        }
    }
}