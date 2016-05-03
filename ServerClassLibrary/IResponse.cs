using System.IO;

namespace ServerClassLibrary {
    public interface IResponse {
        int StatusCode { get; }
        string ReasonPhrase { get; }
        string Version { get; }
        byte[] Body { get; set; }
        void Send(BinaryWriter writer);
        string GetHeader(string contentType);
    }
}