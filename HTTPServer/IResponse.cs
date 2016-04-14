using System.Collections.Generic;
using System.IO;

namespace HTTPServer {
    public interface IResponse {
        int StatusCode { get; }
        string ReasonPhrase { get; }
        string Version { get; }
        byte[] Body { get; set; }
        void Send(Stream ioStream);
        string GetHeader(string contentType);
    }
}