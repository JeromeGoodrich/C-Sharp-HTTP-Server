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
        public void ConfiguredRouterContainsExpectedHandlers() {
            _request.Path = "/image.jpeg";
            var handler = _router.Route(_request);

            Assert.IsType<FileHandler>(handler);
        }
    }
}
