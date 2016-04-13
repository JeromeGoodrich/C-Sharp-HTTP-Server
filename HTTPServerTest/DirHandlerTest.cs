using System;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class DirHandlerTest {
        [Fact]
        public void TestReturnsHtmlListofDirContents() {
            var request = new Request();
            request.SetMethod("GET");
            request.SetPath("/");
            request.SetVersion("HTTP/1.1");

            var handler = new DirHandler();
            var response = handler.Handle(request);
            Assert.Equal(response.GetStatus(), 200);
            Assert.Equal(response.GetVersion(), "HTTP/1.1");
            Assert.Equal(response.GetReasonPhrase(), "OK");
            Assert.Contains("<li><a href=\"file1", Encoding.UTF8.GetString(response.GetBody()));
        }
    }
}