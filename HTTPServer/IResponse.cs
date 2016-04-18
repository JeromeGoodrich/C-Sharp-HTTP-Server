using System.Collections.Generic;
using System.IO;

namespace HTTPServer {
    public interface IResponse {
        int StatusCode { get; }
        string ReasonPhrase { get; }
        string Version { get; }
        byte[] Body { get; set; }
        void Send(BinaryWriter writer);
        string GetHeader(string contentType);
    }
}