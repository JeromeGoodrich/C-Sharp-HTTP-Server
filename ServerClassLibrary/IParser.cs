using System.IO;

namespace ServerClassLibrary {
    public interface IParser {
        Request Parse(StreamReader reader);
    }
}