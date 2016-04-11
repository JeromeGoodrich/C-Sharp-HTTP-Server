using System.Net.Sockets;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockSocket : IClientSocket {
        private bool _closed;

        public bool IsClosed() {
            return _closed;
        }

        public NetworkStream GetStream() {
            return null;
        }

        public void Close() {
            _closed = true;
        }
    }
}