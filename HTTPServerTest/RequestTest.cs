using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RequestTest {
        [Fact]
        public void TestGettersAndSetters() {
            var request = new Request();
            request.SetMethod("GET");
            request.SetPath("/some_path");
            request.SetVersion("HTTP/1.1");
            request.SetHeader("Host", "www.example.com");
            request.SetHeader("Accept", "*/*");
            request.SetBody("firstname=jerome&lastname=goodrich");

            Assert.Equal(request.GetMethod(), "GET");
            Assert.Equal(request.GetPath(), "/some_path");
            Assert.Equal(request.GetVersion(), "HTTP/1.1");
            Assert.Equal(request.GetHeader("Host"), "www.example.com");
            Assert.Equal(request.GetHeader("Accept"), "*/*");
            Assert.Equal(request.GetBody(), "firstname=jerome&lastname=goodrich");
        }
    }
}