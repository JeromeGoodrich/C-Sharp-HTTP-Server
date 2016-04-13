using System;
using System.Collections.Generic;
using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks
{
    internal class MockResponse : IResponse {
        private int _callsToSend;
        private Stream _ioStream;

        public void Send(Stream ioStream) {
            _ioStream = ioStream;
            _callsToSend++;
        }

        public int GetStatus() {
            return 0;
        }

        public string GetVersion() {
            throw new System.NotImplementedException();
        }

        public string GetReasonPhrase(int status) {
            throw new System.NotImplementedException();
        }

        public string GetReasonPhrase() {
            throw new System.NotImplementedException();
        }

        public byte[] GetBody() {
            throw new System.NotImplementedException();
        }

        public IEnumerable<char> GetHeader(string contentType) {
            throw new System.NotImplementedException();
        }

        public int GetCallsToSend() {
            return _callsToSend;
        }

        public Stream GetLastStreamPassedToSend() {
            return _ioStream;
        }

        string IResponse.GetHeader(string contentType)
        {
            throw new NotImplementedException();
        }
    }
}