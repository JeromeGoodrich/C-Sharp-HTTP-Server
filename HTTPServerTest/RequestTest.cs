using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RequestTest {
        private readonly Request _request;

        public RequestTest() {
            _request = new Request();
        }

        [Fact]
        public void TestSetAndGetMethod() {
            _request.Method = "GET";
            Assert.Equal("GET", _request.Method);
        }

        [Fact]
        public void TestSetAndGetPath() {
            _request.Path = "/";
            Assert.Equal("/", _request.Path);
        }

        [Fact]
        public void TestSetAndGetVersion() {
            _request.Version = "HTTP/1.1";
            Assert.Equal("HTTP/1.1", _request.Version);
        }

        [Fact]
        public void TestAddAndGetHeader() {
            _request.AddHeader("Content-Type", "text/plain");
            Assert.Equal("text/plain", _request.GetHeader("Content-Type"));
        }

        [Fact]
        public void TestGetHeaders() {
            _request.AddHeader("Content-Length", "31");
            Assert.True(_request.GetHeaders().ContainsKey("Content-Length"));
            Assert.True(_request.GetHeaders().ContainsValue("31"));
        }

        [Fact]
        public void TestSetAndGetBody() {
            _request.Body = "firstname=jerome&lastname=goodrich";
            Assert.Equal("firstname=jerome&lastname=goodrich", _request.Body);
        }
    }
}