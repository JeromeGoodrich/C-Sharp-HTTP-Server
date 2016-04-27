using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RedirectHandlerTest {
        private IResponse _response;

        public RedirectHandlerTest() {
            var request = new Request {
                Method = "GET",
                Path = "/redirect",
                Version = "HTTP/1.1"
            };
            var handler = new RedirectHandler();
            _response = handler.Handle(request);
        }

        [Fact]
        public void RedirectsReturns302() {
            Assert.Equal(302, _response.StatusCode);
        }

        [Fact]
        public void RedirectsToLocalHostFiveThousand() {
            Assert.Equal("http://localhost:5000/", _response.GetHeader("Location"));
        }
    }
}