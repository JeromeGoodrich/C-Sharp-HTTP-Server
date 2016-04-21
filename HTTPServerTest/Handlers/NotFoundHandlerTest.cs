using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class NotFoundHandlerTest {
        private Request _request;

        public NotFoundHandlerTest() {
            _request = new Request
            {
                Method = "GET",
                Path = "/not_a_path",
                Version = "HTTP/1.1"
            };
        }

        [Fact]
        public void WillHandleTest() {
            var handler = new NotFoundHandler();
            Assert.True(handler.WillHandle(_request.Method, _request.Path));
        }

        [Fact]
        public void Returns404StatusTest() {
            var handler = new NotFoundHandler();
            var response = handler.Handle(_request);

            Assert.Equal(404, response.StatusCode);
        }
    }
}