using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ParserTest {
        private readonly Request _request;

        public ParserTest() {
            var rawRequest = Encoding.UTF8.GetBytes("POST /form HTTP/1.1\r\n" +
                                                 "Host: www.example.com\r\n" +
                                                 "Content-Length: 31\r\n\r\n" +
                                                 "firstname=jerome&lastname=goodrich");
            var stream = new MemoryStream(rawRequest);
            var parser = new Parser();

            _request = parser.Parse(new StreamReader(stream));
        }

        [Fact]
        public void TestParseMethod() {
            Assert.Equal("POST", _request.Method);
        }

        [Fact]
        public void TestParsePath() {
            Assert.Equal("/form", _request.Path);
        }

        [Fact]
        public void TestParseVersion() {
            Assert.Equal("HTTP/1.1", _request.Version);
        }

        [Fact]
        public void TestParseHeaders() {
            Assert.Equal("www.example.com", _request.GetHeader("Host"));
            Assert.Equal("31", _request.GetHeader("Content-Length"));
        }

        [Fact]
        public void TestParseBody() {
            Assert.Equal("firstname=jerome&lastname=goodrich", _request.Body);
        }
    }
}