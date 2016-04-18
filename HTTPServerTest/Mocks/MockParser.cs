using System.IO;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockParser : IParser {

        private Stream _ioStream;
        private readonly Request _request;
        private int _callsToParse;

        public MockParser(Request request)
        {
            _request = request;
        }

        public Request Parse(StreamReader reader) {
            _ioStream = reader.BaseStream;
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