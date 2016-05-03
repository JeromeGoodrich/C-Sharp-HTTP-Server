using System;
using System.IO;
using ServerClassLibrary;
using Xunit;

namespace ServerClassLibraryTest {
    public class RouterTest {

        private string _publicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures");
        private Router _router;
        private Request _request;

        public RouterTest() {
            _router = new Router(_publicDir, new FileLogger());
            _router.AddRoute(new Route("GET", "/foo", new RouterTestHandler(1000)));
            _router.AddRoute(new Route("POST", "/foo", new RouterTestHandler(3000)));
            _router.AddRoute(new Route("GET", "/bar", new RouterTestHandler(4500)));
            _request = new Request()
            {
                Method = "GET",
                Version = "HTTP1.1",
            };
        }

        private class RouterTestHandler : IHandler {
            private int _statusCode;

            internal RouterTestHandler(int statusCode) {
                _statusCode = statusCode;
            }

            public IResponse Handle(Request request) {
                return new Response(_statusCode, request.Version);
            }
        }


        [Fact]
        public void AddRouteTest() {
            var route = new Route("Method", "Path", new RouterTestHandler(200));

            _router.AddRoute(route);

            Assert.True(_router.Routes.Contains(route));
        }

        [Fact]
        public void NotFoundTest() {
            _request.Path = "/booooo";

            Assert.IsType<NotFoundHandler>(_router.Route(_request));
        }

        [Fact]
        public void NotAllowedTest() {
            _request.Method = "BOGUS";
            _request.Path = "/foo";

            Assert.IsType<MethodNotAllowedHandler>(_router.Route(_request));
        }

        [Fact]
        public void RouteTest() {
            _request.Path = "/foo";

            var handler = _router.Route(_request);
            var response = handler.Handle(_request);

            Assert.Equal(1000, response.StatusCode);
        }
    }
}