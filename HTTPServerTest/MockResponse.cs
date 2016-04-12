using System;
using System.Collections.Generic;
using System.IO;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockResponse : IResponse {
        private int _callsToSend;
        private Stream _ioStream;

        public void Send(Stream ioStream) {
            _ioStream = ioStream;
            _callsToSend++;
        }

        public int GetCallsToSend() {
            return _callsToSend;
        }

        public Stream GetLastStreamPassedToSend() {
            return _ioStream;
        }
    }
}