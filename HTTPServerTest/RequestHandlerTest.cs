using System;
using System.Collections.Generic;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RequestHandlerTest {
        [Fact]
        public void Test() {
            var handlers = new List<IHandler> {
                new FakeHandler("/bar",5000),
                new FakeHandler("/foo", 1000)
            };
            var router = new RequestHandler(handlers);
            var request = new Request {
                Method = "GET",
                Path = "/foo",
                Version = "HTTP/1.1"
            };
            IResponse response = router.Handle(request);

            Assert.Equal(1000, response.StatusCode);
        } 


    }
}