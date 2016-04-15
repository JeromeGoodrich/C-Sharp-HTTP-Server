using System.IO;
using System.Net.Sockets;

namespace HTTPServer {
    public class ClientSocket : IClientSocket {
        private readonly TcpClient _tcpClient;

        public ClientSocket(TcpClient tcpClient) {
            _tcpClient = tcpClient;
        }

        public Stream GetStream() {
            return _tcpClient.GetStream();
        }

        public void Close() {
            _tcpClient.Close();
        }
    }
}