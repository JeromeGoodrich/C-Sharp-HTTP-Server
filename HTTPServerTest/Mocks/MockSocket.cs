using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockSocket : IClientSocket {
        private bool _closed;
        private readonly Stream _ioStream;

        public MockSocket(Stream ioStream) {
            _ioStream = ioStream;
        }

        public MockSocket() {}

        public bool IsClosed() {
            return _closed;
        }

        public Stream GetStream() {
            return _ioStream;
        }

        public void Close() {
            _closed = true;
        }
    }
}