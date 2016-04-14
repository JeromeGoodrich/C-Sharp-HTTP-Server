using System.IO;
using System.Net.Sockets;

namespace HTTPServer {
    public class ClientSocket : IClientSocket {
        private TcpClient _tcpClient;

        public ClientSocket(TcpClient tcpClient) {
            _tcpClient = tcpClient;
        }

        public Stream GetStream() {
            throw new System.NotImplementedException();
        }

        public void Close() {
            throw new System.NotImplementedException();
        }
    }
}