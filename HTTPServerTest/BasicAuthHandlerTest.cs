using System;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class BasicAuthHandlerTest {

        private IResponse _response;
        private Request _request;
        private BasicAuthHandler _handler;

        public BasicAuthHandlerTest() {
            _request = new Request()
            {
                Method = "GET",
                Path = "/logs",
                Version = "HTTP/1.1"
            };
            _handler = new BasicAuthHandler();

            
        }

        [Fact]
        public void Returns401WithoutCredentialsTest() {
            _response = _handler.Handle(_request);
            Assert.Equal(401, _response.StatusCode);
        }

        [Fact]
        public void RequiresAuthenticationTest() {
            _response =_handler.Handle(_request);
            Assert.Equal("Basic realm=\"Camelot\"", _response.GetHeader("WWW-Authenticate"));
        }

        [Fact]
        public void BodyHasNoContentTest() {
            _response = _handler.Handle(_request);
            Assert.Equal(null, _response.Body);
        }

        [Fact]
        public void Returns200WithCredentialsTest() {
            _request.AddHeader("Authorization", "Basic YWRtaW46aHVudGVyMg==");
            _response = _handler.Handle(_request);

            Assert.Equal(200, _response.StatusCode);
        }


    }

}