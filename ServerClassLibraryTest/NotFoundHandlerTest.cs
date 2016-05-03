using ServerClassLibrary;
using Xunit;

namespace ServerClassLibararyTest {
    public class NotFoundHandlerTest {
        private readonly Request _request;

        public NotFoundHandlerTest() {
            _request = new Request
            {
                Method = "GET",
                Path = "/not_a_path",
                Version = "HTTP/1.1"
            };
        }

        [Fact]
        public void Returns404StatusTest() {
            var handler = new NotFoundHandler();
            var response = handler.Handle(_request);

            Assert.Equal(404, response.StatusCode);
        }
    }
}