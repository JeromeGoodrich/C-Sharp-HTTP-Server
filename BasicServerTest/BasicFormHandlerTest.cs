using Xunit;
using ServerClassLibrary;
using BasicServer;
using System.Text;

namespace BasicServerTest {
    public class BasicFormHandlerTest {

        private Request _request = new Request() {
            Method = "GET",
            Path = "/form",
            Version = "HTTP/1.1"
        };
        private IHandler _handler = new BasicFormHandler();
        private IResponse _response;


        [Fact]
        public void GetReturns200() {
            _response = _handler.Handle(_request);

            Assert.Equal(200, _response.StatusCode);
        }

        [Fact]
        public void PostReturns200() {
            _request.Method = "POST";
            _response = _handler.Handle(_request);

            Assert.Equal(200, _response.StatusCode);
        }

        [Fact]
        public void GetContainsHtmlForm() {
            _response = _handler.Handle(_request);

            Assert.Contains("<form method" , Encoding.UTF8.GetString(_response.Body));
        }

        [Fact]
        public void GetContainsValueAfterPost() {
            _request.Method = "POST";
            _request.Body = "name=juice";
            _handler.Handle(_request);

            _request.Method = "GET";
            _request.Body = null;
            _response = _handler.Handle(_request);

            Assert.Contains("juice", Encoding.UTF8.GetString(_response.Body));
        }
    }
}
