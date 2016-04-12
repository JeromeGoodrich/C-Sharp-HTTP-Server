using System;
using System.Collections.Generic;
using System.Net.Sockets;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockHandler : IHandler {
        private MockResponse _mockResponse;
        private int _callsToHandle = 0;

        public MockHandler(MockResponse mockResponse) { 
            this._mockResponse = mockResponse;
        }

        public int GetCallsToHandle() {
            return _callsToHandle;
        }
    
        public IResponse Handle(Request request) {
            _callsToHandle++;
            return _mockResponse;
        }
    }
}