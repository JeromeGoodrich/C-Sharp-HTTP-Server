using System.Net.Sockets;

namespace HTTPServer {
    public class Request
    {
        private NetworkStream networkStream;

        public Request()
        {
        }

        public Request(NetworkStream networkStream)
        {
            this.networkStream = networkStream;
        }
    }
}