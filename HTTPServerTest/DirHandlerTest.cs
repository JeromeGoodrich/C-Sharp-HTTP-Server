using System;
using System.IO;
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
            var publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures\");
            var handler = new DirHandler(publicDir);

            var response = handler.Handle(request);

            Assert.Equal(response.GetStatus(), 200);
            Assert.Equal(response.GetVersion(), "HTTP/1.1");
            Assert.Equal(response.GetReasonPhrase(), "OK");
            Assert.Contains("<li><a href=\"/file1", Encoding.UTF8.GetString(response.GetBody()));
        }

        [Fact]
        public void TestReturnsJsonListofDirContents() {
            var request = new Request();
            request.SetMethod("GET");
            request.SetPath("/");
            request.SetVersion("HTTP/1.1");
            request.SetHeader("Accept", "application/json");

            var publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures\");
            var handler = new DirHandler(publicDir);
            var response = handler.Handle(request);
            Assert.Equal(response.GetStatus(), 200);
            Assert.Equal(response.GetVersion(), "HTTP/1.1");
            Assert.Equal(response.GetReasonPhrase(), "OK");
            Assert.Equal(response.GetHeader("Content-Type"), "application/json");
            Assert.Equal(response.GetHeader("Content-Length"), "144");
            Assert.Contains("{ files : [", Encoding.UTF8.GetString(response.GetBody()));
        }
    }
}