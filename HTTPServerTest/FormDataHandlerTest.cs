using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class FormDataHandlerTest {
        [Fact]
        public void GetAfterPostHasBodyContent() {
            var postRequest = new Request {
                Method = "POST",
                Path = "/form",
                Version = "HTTP/1.1",
                Body = "data=fatcat"
            };
            var handler = new FormDataHandler();
            handler.Handle(postRequest);
            var getRequest = new Request {
                Method = "GET",
                Path = "/form",
                Version = "HTTP/1.1"
            };
            var response = handler.Handle(getRequest);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("data=fatcat", Encoding.UTF8.GetString(response.Body));
        } 
    }
}