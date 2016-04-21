using System;
using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class FileHandlerTest {
        private readonly string _publicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures\");
        private readonly Request _request;
        private readonly FileHandler _handler;

        public FileHandlerTest() {
            _request = new Request()
            {
                Method = "GET",
                Path = "/file1",
                Version = "HTTP/1.1."
            };
            _handler = new FileHandler(_publicDir);
        }

        [Fact]
        public void WillHandleTest() { 
            Assert.True(_handler.WillHandle(_request.Method, _request.Path));
        }

        [Fact]
        public void WillNotHandleTest() {
            var badRequest = new Request()
            {
                Method = "GET",
                Path = "/foobar",
                Version = "HTTP/1.1."
            };
            var handler = new FileHandler(_publicDir);
            Assert.False(handler.WillHandle(badRequest.Method, badRequest.Path));
        }

        [Fact]
        public void RequestForTextReturns200() {
            var response = _handler.Handle(_request);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void RequestForTextReturnsCorrectHeaders()
        {
            var response = _handler.Handle(_request);
            Assert.Equal("14", response.GetHeader("Content-Length"));
            Assert.Equal("text/plain", response.GetHeader("Content-Type"));
        }


        [Fact]
        public void ContentofTextFileIsInResponseBodyTest() {
            var response = _handler.Handle(_request);
            Assert.Equal("file1 contents", Encoding.UTF8.GetString(response.Body));
        }

    

        [Fact]
        public void ContentofImageIsInResponseBodyTest()
        {
            var request = new Request()
            {
                Method = "GET",
                Path = "/file1",
                Version = "HTTP/1.1."
            };
            var handler = new FileHandler(_publicDir);
            var response = handler.Handle(request);
            Assert.Equal("file1 contents", Encoding.UTF8.GetString(response.Body));

        }
    }
}