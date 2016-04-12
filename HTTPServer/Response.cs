using System.IO;
using System.Net.Sockets;

namespace HTTPServer {
    public class Response : IResponse {
        public void Send(Stream ioStream) {
            throw new System.NotImplementedException();
        }
    }
}