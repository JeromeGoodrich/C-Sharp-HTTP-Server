using CobSpecServer;
using ServerClassLibrary;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace CobSpecServerTest {
    public class CobSpecRouterTest {

        private readonly string _publicDir = @"C:\Users\jgoodrich\Documents\Visual Studio 2015\Projects\HTTPServer\HTTPServerTest\Fixtures";
        private readonly string _logFile = @"C:\Users\jgoodrich\Documents\Visual Studio 2015\Projects\HTTPServer\logFile.txt";
        private Request _request;
        private Router _router;

        public CobSpecRouterTest(){
            _router = CobSpecRouter.Configure(_publicDir, _logFile); 
            _request = new Request() {
                Method ="GET",
                Version = "HTTP/1.1"
            };
        }

        [Fact]
        public void ConfiguredRouterRoutesToFileHandler() {
            _request.Path = "/image.jpeg";
            var handler = _router.Route(_request);

            Assert.IsType<FileHandler>(handler);
        }

        [Fact]
        public void ConfiguredRouterRoutesToDirHandler() {
            _request.Path = "/";
            var handler = _router.Route(_request);

            Assert.IsType<DirHandler>(handler);
        }

        [Fact]
        public void ConfiguredRouterRoutesToFormDataHandler() {
            _request.Path = "/form";
            var handler = _router.Route(_request);

            Assert.IsType<FormDataHandler>(handler);
        }

        [Fact]
        public void ConfiguredRouterRoutesToOptionsHandler() {
            _request.Path = "/method_options";
            var handler = _router.Route(_request);

            Assert.IsType<OptionsHandler>(handler);
        }

        [Fact]
        public void ConfiguredRouterRoutesToBasicAuthHandler() {
            _request.Path = "/logs";
            var handler = _router.Route(_request);

            Assert.IsType<BasicAuthHandler>(handler);
        }

    }
}
