using System;
using System.IO;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class RouterTest {

        private readonly Router _router;
        private readonly Request _request;

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
        public void RequestToRootReturnsDirHandler() {
            _request.Path = "/";

            Assert.IsType<DirHandler>(_router.Route(_request));
        }

        [Fact]
        public void RequestToFileReturnsFileHandler() {
            _request.Path = "/file1";

            Assert.IsType<FileHandler>(_router.Route(_request));
        }

        [Fact]
        public void RequestToLogsReturnsBasicAuthHandler() {
            _request.Path = "/logs";

            Assert.IsType<BasicAuthHandler>(_router.Route(_request));
        }

        [Fact]
        public void ReturnsNotFoundHandler() {
            _request.Path = "/foo";

            Assert.IsType<NotFoundHandler>(_router.Route(_request));
        }

        [Fact]
        public void RequestToParametersReturnsParamsHandler() {
            _request.Path = "/parameters";

            Assert.IsType<ParamsHandler>(_router.Route(_request));
        }

        [Fact]
        public void RequestToFormReturnsFormDataHandler() {
            _request.Path = "/form";

            Assert.IsType<FormDataHandler>(_router.Route(_request));
        }

        [Fact]
        public void RequestToRedirectReturnsRedirectHandler()
        {
            _request.Path = "/redirect";

            Assert.IsType<RedirectHandler>(_router.Route(_request));
        }
    }
}