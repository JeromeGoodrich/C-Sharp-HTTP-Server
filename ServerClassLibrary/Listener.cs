using System.Net;
using System.Net.Sockets;

namespace ServerClassLibrary {
    public class Listener : IListener {
        private readonly TcpListener _listener;

        public Listener(IPAddress ip, int port) {
            _listener = new TcpListener(ip, port);
        }

        public IClientSocket Accept() {
            var tcpClient = _listener.AcceptTcpClient();
            return new ClientSocket(tcpClient);
        }

        public void Start() {
            _listener.Start();
        }
    }
}