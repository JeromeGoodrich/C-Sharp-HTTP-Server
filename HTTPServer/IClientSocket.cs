using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace HTTPServer {
    public interface IClientSocket
    {
        Stream GetStream();
        void Close();
    }
}