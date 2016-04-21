using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockSocket : IClientSocket {
        private readonly Stream _ioStream;
        private bool _closed;

        public MockSocket(Stream ioStream) {
            _ioStream = ioStream;
        }

        public MockSocket() {}

        public Stream GetStream() {
            return _ioStream;
        }

        public void Close() {
            _closed = true;
        }

        public bool IsClosed() {
            return _closed;
        }
    }
}