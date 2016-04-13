using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ParserTest {

        [Fact]
        public void TestParseGet()
        {
            var rawData = Encoding.UTF8.GetBytes("GET / HTTP/1.1\r\n" +
                                                 "Host: www.example.com\r\n\r\n");
            var stream = new MemoryStream(rawData);
            var parser = new Parser();
            var request = parser.Parse(stream);

            Assert.Equal(request.GetMethod(), "GET");
            Assert.Equal(request.GetPath(), "/");
            Assert.Equal(request.GetVersion(), "HTTP/1.1");
            Assert.Equal(request.GetHeader("Host"), "www.example.com");
        }

//        [Fact]
//        public void Test() {
//            var rawData = Encoding.UTF8.GetBytes("POST /form HTTP/1.1\r\n" +
//                                                 "Host: www.example.com\r\n" +
//                                                 "Content-Length: 31\r\n\r\n" +
//                                                 "firstname=jerome&lastname=goodrich");
//            var stream = new MemoryStream(rawData);
//            var parser = new Parser();
//            var request = parser.Parse(stream);
//
//            Assert.Equal(request.GetMethod(), "GET");
//            Assert.Equal(request.GetPath(), "/form");
//            Assert.Equal(request.GetVersion(), "HTTP/1.1");
//            Assert.Equal(request.GetHeader("Host"), "www.example.com");
//            Assert.Equal(request.GetHeader("Content-Length"), "31");
//            Assert.Equal(request.GetBody(), "firstname=jerome&lastname=goodrich");
//        }
    }
}