using System;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class BasicAuthHandlerTest {
        [Fact]
        public void EnsureLogsHaveBasicAuth() {
            var request = new Request() {
                Method = "GET",
                Path = "/logs",
                Version = "HTTP/1.1"
            };
            var handler = new BasicAuthHandler();

            var response = handler.Handle(request);

            Assert.Equal(401, response.StatusCode);
        }
    }

}