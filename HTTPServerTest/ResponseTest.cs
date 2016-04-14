using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ResponseTest {
        [Fact]
        public void TestGetters() {
            var response = new Response(200, "HTTP/1.1");
            response.AddBody(Encoding.UTF8.GetBytes("response body"));
            response.AddHeader("Content-Type", "text/plain");
            //split these up into separate tests.

            Assert.Equal(response.GetStatus(), 200);
            Assert.Equal(response.GetReasonPhrase(), "OK");
            Assert.Equal(response.GetVersion(), "HTTP/1.1");
            Assert.Equal(response.GetHeader("Content-Type"), "text/plain");
            Assert.Equal(Encoding.UTF8.GetString(response.GetBody()), "response body");
        }

        [Fact]
        public void TestSend() {
            var response = new Response(200, "HTTP/1.1");
            var ioStream = new MemoryStream();
            response.AddBody(Encoding.UTF8.GetBytes("response body"));
            response.AddHeader("Content-Type", "text/plain");
            response.Send(ioStream);
            var reader = new StreamReader(ioStream);
            ioStream.Seek(0, SeekOrigin.Begin);

            Assert.Equal(response.GetBody().Length, 13);
            Assert.Contains("HTTP/1.1" , reader.ReadToEnd());
        }
    }

} ;