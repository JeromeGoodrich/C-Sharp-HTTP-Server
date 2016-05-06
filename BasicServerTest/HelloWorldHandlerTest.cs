using Xunit;
using ServerClassLibrary;
using BasicServer;
using System.Text;

namespace BasicServerTest {
    public class HelloWorldHandlerTest {

        private Request _request = new Request() {
                Method = "GET",
                Path = "/",
                Version = "HTTP/1.1"
            };
        private IHandler _handler = new HelloWorldHandler();
        private IResponse _response;


        [Fact]
        public void returns200() {
            _response = _handler.Handle(_request);

            Assert.Equal(200, _response.StatusCode);
        }

        [Fact]
        public void HelloWorldBodyTest() {
            _response = _handler.Handle(_request);

            Assert.Equal("Hello World", Encoding.UTF8.GetString(_response.Body));
        }


    }
}
