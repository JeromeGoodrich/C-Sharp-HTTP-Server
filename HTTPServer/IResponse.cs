using System.Collections.Generic;
using System.IO;

namespace HTTPServer {
    public interface IResponse {
        void Send(Stream ioStream);
        int GetStatus();
        string GetVersion();
        string GetReasonPhrase();
        byte[] GetBody();
        string GetHeader(string contentType);
    }
}