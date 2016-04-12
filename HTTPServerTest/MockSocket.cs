using System.IO;
using System.Net.Sockets;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockSocket : IClientSocket {
        private bool _closed;
        private readonly Stream _ioStream;

        public MockSocket(Stream ioStream)
        {
            this._ioStream = ioStream;
        }

        public MockSocket()
        {
        }

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