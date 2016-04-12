using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks
{
    internal class MockParser : IParser
    {
        private readonly Stream _ioStream;
        private readonly Request _request;
        private int _callsToParse;

        public MockParser(Stream ioStream, Request request)
        {
            _ioStream = ioStream;
            _request = request;
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