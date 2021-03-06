﻿using System.IO;
using ServerClassLibrary;

namespace ServerClassLibraryTest.Mocks {
    public class MockResponse : IResponse {
        private int _callsToSend;
        private Stream _ioStream;

        public int StatusCode { get; }
        public string ReasonPhrase { get; }
        public string Version { get; }
        public byte[] Body { get; set; }

        public void Send(BinaryWriter writer) {
            _ioStream = writer.BaseStream;
            _callsToSend++;
        }

        public string GetHeader(string contentType) {
            return null;
        }

        public int GetCallsToSend() {
            return _callsToSend;
        }

        public Stream GetLastStreamPassedToSend() {
            return _ioStream;
        }
    }
}