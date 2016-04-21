using System;
using System.Collections.Generic;
using HTTPServer;
using Xunit;

namespace HTTPServerTest.Router {
    public class RouterTest {
        [Fact]
        public void Test() {
            var handlers = new List<IHandler> {
                new FakeHandler("/bar",5000),
                new FakeHandler("/foo", 1000)
            };
            var router = new HTTPServer.Router.Router(handlers);
            var request = new Request {
                Method = "GET",
                Path = "/foo",
                Version = "HTTP/1.1"
            };
            IResponse response = router.Route(request);

            Assert.Equal(1000, response.StatusCode);
        } 


    }
}