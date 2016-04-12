using System.Collections.Generic;
using System.Net.Sockets;

namespace HTTPServer {
    public interface IHandler
    {
        int GetCallsToHandle();
        IResponse Handle(Request request);
    }
}