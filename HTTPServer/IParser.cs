using System.IO;

namespace HTTPServer {
    public interface IParser {
        Request Parse(Stream stream);
    }
}