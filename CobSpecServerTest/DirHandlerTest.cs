using System;
using System.IO;
using System.Text;
using ServerClassLibrary;
using CobSpecServer;
using Xunit;

namespace CobSpecServerTest {
    public class DirHandlerTest {
        private readonly DirHandler _handler;
        private readonly Request _request;
        private IResponse _response;

        public DirHandlerTest() {
            _request = new Request {
                Method = "GET",
                Path = "/",
                Version = "HTTP/1.1"
            };
            var publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures");;
            _handler = new DirHandler(publicDir);
        }

        [Fact]
        public void TestReturnsHtmlListofDirContents() {
            _request.AddHeader("Accept", "*/*");
            _response = _handler.Handle(_request);

            Assert.Contains("<li><a href=\"/file1", Encoding.UTF8.GetString(_response.Body));
        }

        [Fact]
        public void TestReturnsJsonListofDirContents() {
            _request.AddHeader("Accept", "application/json");
            _response = _handler.Handle(_request);

            Assert.Contains("{ files : [big", Encoding.UTF8.GetString(_response.Body));
        }
    }
}