using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ResponseTest {
        public void TestGetters() {
            var response = new Response(200, "HTTP/1.1");
            response.AddBody(Encoding.UTF8.GetBytes("response body"));
            response.AddHeader("Content-Type", "text/plain");

            Assert.Equal(response.GetStatus(), 200);
            Assert.Equal(response.GetReasonPhrase(), "OK");
            Assert.Equal(response.GetVersion(), "HTTP/1.1");
            Assert.Equal(response.GetHeader("Content-Type"), "text/plain");
            Assert.Equal(Encoding.UTF8.GetString(response.GetBody()), "response body");
        }
    }

}