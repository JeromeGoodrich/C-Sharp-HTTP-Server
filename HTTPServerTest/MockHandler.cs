using System;
using System.Collections.Generic;
using System.Net.Sockets;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockHandler : IHandler {
        private int _callsToHandle = 0;

        public int GetCallsToHandle() {
            return _callsToHandle;
        }
    
        public Response Handle(Request request) {
            _callsToHandle++;
            return null;
        }
    }
}