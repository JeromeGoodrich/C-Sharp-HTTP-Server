using System.Collections.Generic;
using System.Net.Sockets;

namespace HTTPServer {
    public interface IClientSocket
    {
        NetworkStream GetStream();
        void Close();
    }
}