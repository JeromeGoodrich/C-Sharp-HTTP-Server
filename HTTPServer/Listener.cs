using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    public class Listener : IListener {
        private readonly TcpListener _listener;

        public Listener(IPAddress ip, int port) {
        _listener = new TcpListener(ip ,port);
        }

        public bool Listening() {
            return true;
        }

        public async Task<IClientSocket> AcceptAsync() {
            var tcpClient = await _listener.AcceptTcpClientAsync();

//            var connected = tcpClient.Connected;            
//            var stream = tcpClient.GetStream();
//            var reader = new BinaryReader(stream);
//
//            if (stream.CanRead) {
//                byte[] bytes = new byte[1024];
//                var sb = new StringBuilder();
//                int bytesRead = 0;
//                do {
//                    bytesRead = stream.Read(bytes, 0, bytes.Length);
//                    sb.Append(Encoding.UTF8.GetString(bytes, 0, bytesRead));
//                } while (stream.DataAvailable);
//                var message = sb.ToString();
//
//            }
//            else {
//                Console.WriteLine("SORRY!");
//            }
            return new ClientSocket(tcpClient);
        }

        public void Start() {
            _listener.Start();
            System.Console.WriteLine(_listener.LocalEndpoint.ToString());
        }
    }
}