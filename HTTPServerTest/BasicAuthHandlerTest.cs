using System;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class BasicAuthHandlerTest {

        private IResponse _response;

        public BasicAuthHandlerTest() {
            var request = new Request()
            {
                Method = "GET",
                Path = "/logs",
                Version = "HTTP/1.1"
            };
            var handler = new BasicAuthHandler();

            _response = handler.Handle(request);
        }

        [Fact]
        public void Returns401Test() {
            Assert.Equal(401, _response.StatusCode);
        }

        [Fact]
        public void RequiresAuthenticationTest() {
            Assert.Equal("Basic realm=\"Camelot\"", _response.GetHeader("WWW-Authenticate"));
        }
    }

}