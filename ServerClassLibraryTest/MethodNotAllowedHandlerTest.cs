using Xunit;
using ServerClassLibrary;

namespace ServerClassLibraryTest {
    public class MethodNotAllowedHandlerTest{

        [Fact]
        public void Returns405StatusCode() {
            var request = new Request() {
                Method = "NotAMethod",
                Path = "/foo",
                Version = "HTTP/1.1"
            };
            var handler = new MethodNotAllowedHandler();
            var response = handler.Handle(request);

            Assert.Equal(405, response.StatusCode);
        }
    }
}
