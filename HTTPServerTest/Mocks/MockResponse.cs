using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockResponse : IResponse {
        private int _callsToSend;
        private Stream _ioStream;

        public int StatusCode { get; }
        public string ReasonPhrase { get; }
        public string Version { get; }
        public byte[] Body { get; set; }

        public void Send(Stream ioStream) {
            _ioStream = ioStream;
            _callsToSend++;
        }

        public int GetCallsToSend() {
            return _callsToSend;
        }

        public Stream GetLastStreamPassedToSend() {
            return _ioStream;
        }

        public string GetHeader(string contentType) {
            return null;
        }
    }
}