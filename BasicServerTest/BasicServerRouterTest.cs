using Xunit;
using BasicServer;
using ServerClassLibrary;

namespace BasicServerTest {
    public class BasicServerRouterTest {

        private Request _request;
        private Router _router;

        public BasicServerRouterTest() {
            _router = BasicServerRouter.Configure();
            _request = new Request() {
                Method = "GET",
                Version = "HTTP/1.1"
            };
        }

        [Fact]
        public void ConfiguredRouterRoutesToHelloWorld() {
            _request.Path = "/";
            var handler = _router.Route(_request);

            Assert.IsType<HelloWorldHandler>(handler);
        }

        [Fact]
        public void ConfiguredRouterRoutesToForm() {
            _request.Path = "/form";
            var handler = _router.Route(_request);

            Assert.IsType<BasicFormHandler>(handler);
        }

    }
}
