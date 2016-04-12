using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using HTTPServer;

namespace HTTPServerTest
{
    internal class MockParser : IParser
    {
        private readonly Stream _ioStream;
        private readonly Request _request;
        private int _callsToParse;

        public MockParser(Stream ioStream, Request request)
        {
            this._ioStream = ioStream;
            this._request = request;
        }

        public Request Parse(Stream stream) {
            _callsToParse++;
            return _request;
        }

        public int GetCallsToParse() {
            return _callsToParse;
        }

        public Stream GetLastStreamPassedToParse() {
            return _ioStream;
        }
    }
}