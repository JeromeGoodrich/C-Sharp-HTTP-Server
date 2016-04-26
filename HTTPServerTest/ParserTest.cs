using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ParserTest {
        private readonly Request _request;

        public ParserTest() {
            var rawRequest = Encoding.UTF8.GetBytes("GET /parameters?variable_1=Operators%20%3C%2C%20%" +
                                                    "3E%2C%20%3D%2C%20!%3D%3B%20%2B%2C%20-%2C%20*%2C%20%26%2" +
                                                    "C%20%40%2C%20%23%2C%20%24%2C%20%5B%2C%20%5D%3A%20%22is%20that" +
                                                    "%20all%22%3F&variable_2=stuff HTTP/1.1\r\n" +
                                                    "Host: www.example.com\r\n" +
                                                    "Content-Length: 34\r\n\r\n" +
                                                    "firstname=jerome&lastname=goodrich");
            var stream = new MemoryStream(rawRequest);
            var parser = new Parser();

            _request = parser.Parse(new StreamReader(stream));
        }

        [Fact]
        public void TestParseMethod() {
            Assert.Equal("GET", _request.Method);
        }

        [Fact]
        public void TestParsePath() {
            Assert.Equal("/parameters", _request.Path);
        }

        [Fact]
        public void TestParseVersion() {
            Assert.Equal("HTTP/1.1", _request.Version);
        }

        [Fact]
        public void TestParseHeaders() {
            Assert.Equal("www.example.com", _request.GetHeader("Host"));
            Assert.Equal("34", _request.GetHeader("Content-Length"));
        }

        [Fact]
        public void TestParseBody() {
            Assert.Equal("firstname=jerome&lastname=goodrich", _request.Body);
        }

        [Fact]
        public void TestParseParams() {
            Assert.True(_request.GetParameters().ContainsKey("variable_1"));
            Assert.True(_request.GetParameters().ContainsValue("stuff"));

        }
    }
}