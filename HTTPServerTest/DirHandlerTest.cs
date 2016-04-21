using System;
using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class DirHandlerTest {
        private readonly Request _request;
        private IResponse _response;
        private readonly DirHandler _handler;

        public DirHandlerTest() {
            _request = new Request {
                Method = "GET",
                Path = "/",
                Version = "HTTP/1.1"
            };
            var publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\..\HTTPServerTest\Fixtures\");
            _handler = new DirHandler(publicDir);            
        }

        [Fact]
        public void TestReturnsHtmlListofDirContents() {
            _response = _handler.Handle(_request);

            Assert.Contains("<li><a href=\"/file1", Encoding.UTF8.GetString(_response.Body));
        }

        [Fact]
        public void TestReturnsJsonListofDirContents() {
            _request.AddHeader("Accept" , "application/json");
            _response = _handler.Handle(_request);

            Assert.Contains("{ files : [big", Encoding.UTF8.GetString(_response.Body));
        }
    }
}