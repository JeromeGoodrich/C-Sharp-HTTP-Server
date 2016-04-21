using System;
using System.IO;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class FileHandlerTest {
        private readonly string _publicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures\");

        [Fact]
        public void WillHandleTest() {
            var request = new Request() {
                Method = "GET",
                Path = "/file1",
                Version = "HTTP/1.1."
            };
            var handler = new FileHandler(_publicDir);
            Assert.True(handler.WillHandle(request.Method, request.Path));
        }

        [Fact]
        public void WillNotHandleTest() {
            var request = new Request()
            {
                Method = "GET",
                Path = "/foobar",
                Version = "HTTP/1.1."
            };
            var handler = new FileHandler(_publicDir);
            Assert.False(handler.WillHandle(request.Method, request.Path));
        }
    }
}