using System.IO;

namespace HTTPServer {
    public interface IClientSocket {
        Stream GetStream();
        void Close();
    }
}