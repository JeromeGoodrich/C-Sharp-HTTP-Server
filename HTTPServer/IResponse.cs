using System.IO;

namespace HTTPServer {
    public interface IResponse {
        void Send(Stream ioStream);
    }
}