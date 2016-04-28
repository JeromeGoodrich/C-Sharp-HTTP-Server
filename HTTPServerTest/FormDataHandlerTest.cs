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
            Assert.Contains("data=fatcat", Encoding.UTF8.GetString(response.Body));
        }

        [Fact]
        public void PutGetDeleteGet() {
            var postRequest = new Request {
                Method = "PUT",
                Path = "/form",
                Version = "HTTP/1.1",
                Body = "data=heathcliff"
            };
            var handler = new FormDataHandler();
            handler.Handle(postRequest);
            var getRequest = new Request
            {
                Method = "GET",
                Path = "/form",
                Version = "HTTP/1.1"
            };
            var response = handler.Handle(getRequest);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("data=heathcliff", Encoding.UTF8.GetString(response.Body));

            var deleteRequest = new Request {
                Method = "DELETE",
                Path = "/form",
                Version = "HTTP/1.1"
            };
            handler.Handle(deleteRequest);
            var getAgainRequest = new Request {
                Method = "GET",
                Path = "/form",
                Version = "HTTP/1.1"
            };
            var newResponse = handler.Handle(getAgainRequest);

            Assert.Null(newResponse.Body);
        }
    }
}