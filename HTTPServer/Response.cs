using System.IO;

namespace HTTPServer {
    public class Response : IResponse {
        private readonly int _status;
        private readonly string _version;
        private byte[] _body;

        public Response(int status, string version) {
            _status = status;
            _version = version;
        }

        public void Send(Stream ioStream) {
            throw new System.NotImplementedException();
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

        public void AddBody(byte[] body) {
            _body = body;
        }
    }
}