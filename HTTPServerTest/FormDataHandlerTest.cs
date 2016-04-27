using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class FormDataHandlerTest {
        [Fact]
        public void GetRequestToFormReturns200() {
            var request = new Request() {
                Method = "GET",
                Path = "/form",
                Version = "HTTP/1.1"
            };
            var handler = new FormDataHandler();
            var response = handler.Handle(request);

            Assert.Equal(200, response.StatusCode);
        } 
    }
}