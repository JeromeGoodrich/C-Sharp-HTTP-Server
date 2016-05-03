using System.IO;

namespace ServerClassLibrary {
    public interface IClientSocket {
        Stream GetStream();
        void Close();
    }
}