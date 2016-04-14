using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HTTPServer {
    public class Response : IResponse {
        private readonly int _status;
        private readonly string _version;
        private byte[] _body;
        private readonly Dictionary<string,string> _headers = new Dictionary<string, string>();

        public Response(int status, string version) {
            _status = status;
            _version = version;
        }

        public void Send(Stream ioStream) {
            var statusLine = _version + " " + _status + " " + GetReasonPhrase() + "\r\n";
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
            if (_body != null) {
                ioStream.Write(_body, 0, _body.Length);
            }
        }

        public int GetStatus() {
            return _status;
        }

        public string GetVersion() {
            return _version;
        }

        public string GetReasonPhrase() {
            return Status.StatusDictionary[_status];
        }

        public byte[] GetBody() {
            return _body;
        }

        public string GetHeader(string headerName) {
            return _headers[headerName];
        }

        public void AddBody(byte[] body) {
            _body = body;
        }

        public void AddHeader(string headerName, string headerValue) {
            _headers.Add(headerName, headerValue);
        }
    }
}