using CobSpecServer;
using ServerClassLibrary;
using Xunit;

namespace CobSpecServerTest {
    public class OptionsHandlerTest {
        private readonly IResponse _response;

        public OptionsHandlerTest() {
            var request = new Request {
                Method = "OPTIONS",
                Path = "/method_options",
                Version = "HTTP/1.1"
            };
            var handler = new OptionsHandler();
            _response = handler.Handle(request);
        }

        [Fact]
        public void RequestToMethodOptionsReturn200() {
            Assert.Equal(200, _response.StatusCode);
        }

        [Fact]
        public void ResponseAllowHeaderHasCorrectContent() {
            Assert.Equal("GET,HEAD,POST,OPTIONS,PUT", _response.GetHeader("Allow"));
        }
    }
}