using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class BasicAuthHandlerTest {
        private readonly BasicAuthHandler _handler;
        private readonly Request _request;

        private IResponse _response;

        public BasicAuthHandlerTest() {
            _request = new Request {
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
            var logger = new FileLogger();
            logger.Log("GET/log HTTP/1.1");
            logger.Log("PUT /these HTTP/1.1");
            logger.Log("HEAD /requests HTTP/1.1");

            _response = _handler.Handle(_request);


            Assert.Contains("GET /log HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
            Assert.Contains("PUT /these HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
            Assert.Contains("HEAD /requests HTTP/1.1", Encoding.UTF8.GetString(_response.Body));
        }

        [Fact]
        public void WillHandleTest()
        {
            Assert.True(_handler.WillHandle(_request.Method, _request.Path));
        }

        [Fact]
        public void WillNotHandleTest()
        {
            var badRequest = new Request
            {
                Method = "GET",
                Path = "/not_a_directory",
                Version = "HTTP/1.1"
            };
            Assert.False(_handler.WillHandle(badRequest.Method, badRequest.Path));
        }
    }
}