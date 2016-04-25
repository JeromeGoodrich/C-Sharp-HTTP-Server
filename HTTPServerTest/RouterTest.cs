using System;
using System.IO;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RouterTest {

        private readonly Router _router;
        private Request _request;

        public RouterTest() {
            var publicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures").Normalize();
            _router = new Router(publicDir);
            _request = new Request() {
                Method = "GET",
                Version = "HTTP1.1"
            };
        }

        [Fact]
        public void ReturnsProperHandler() {
            _request.Path = "/";

            Assert.IsType<DirHandler>(_router.Route(_request));
        }

        [Fact]
        public void ReturnsNotFoundHandler() {
            _request.Path = "/foo";

            Assert.IsType<NotFoundHandler>(_router.Route(_request));
        }



    }
}