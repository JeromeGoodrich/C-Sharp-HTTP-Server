using System.IO;
using System.Net.Sockets;

namespace HTTPServer {
    public interface IParser {
        Request Parse(Stream stream);
    }
}