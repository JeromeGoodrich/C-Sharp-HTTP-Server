using System.Text;
using ServerClassLibrary;
using CobSpecServer;
using Xunit;
using System.IO;
using System;

namespace CobSpecServerTest {
    public class BasicAuthHandlerTest {
        private readonly BasicAuthHandler _handler;
        private readonly Request _request;
        private readonly string _logFile =  Path.Combine(Environment.CurrentDirectory, @"..\..\..\logs\testlog.txt");

        private IResponse _response;

        public BasicAuthHandlerTest() {
            _request = new Request {
                Method = "GET",
                Path = "/logs",
                Version = "HTTP/1.1"
            };
            _handler = new BasicAuthHandler(_logFile);
        }

        [Fact]
        public void Returns401WithoutCredentialsTest() {
            _response = _handler.Handle(_request);

            Assert.Equal(401, _response.StatusCode);
        }

        [Fact]
        public void RequiresAuthenticationTest() {
            _response = _handler.Handle(_request);

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

        [Fact]
        public void Returns401WithIncorrectCredentialsTest() {
            _request.AddHeader("Authorization", "Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==");
            _response = _handler.Handle(_request);

            Assert.Equal(401, _response.StatusCode);
        }

        [Fact]
        public void AuthorizedRequestHasBody() {
            _request.AddHeader("Authorization", "Basic YWRtaW46aHVudGVyMg==");
            var logger = new FileLogger(_logFile);
            logger.Log("GET /log HTTP/1.1");
            logger.Log("PUT /these HTTP/1.1");
            logger.Log("HEAD /requests HTTP/1.1");

            _response = _handler.Handle(_request);


            Assert.Contains("GET /log HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
            Assert.Contains("PUT /these HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
            Assert.Contains("HEAD /requests HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
        }
    }
}