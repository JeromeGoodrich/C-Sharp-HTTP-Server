using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ResponseTest {
        private readonly Response _response;
        private MemoryStream _ioStream;
        private StreamReader _reader;

        public ResponseTest() {
            _response = new Response(200, "HTTP/1.1");
        }

        [Fact]
        public void TestGetStatus() {
            Assert.Equal(200, _response.StatusCode);
        }

        [Fact]
        public void TestGetReasonPhrase() {
            Assert.Equal("OK", _response.ReasonPhrase);
        }

        [Fact]
        public void TestGetVersion() {
            Assert.Equal("HTTP/1.1", _response.Version);
        }

        [Fact]
        public void TestAddAndGetHeader() {
            _response.AddHeader("Content-Type", "text/plain");
            Assert.Equal("text/plain", _response.GetHeader("Content-Type"));
        }

        [Fact]
        public void TestAddAndGetBody() {
            _response.Body = Encoding.UTF8.GetBytes("response body");
            Assert.Equal("response body", Encoding.UTF8.GetString(_response.Body));
        }


        private void SendToClientTestSetUp() {
            _ioStream = new MemoryStream();
            _response.Body = Encoding.UTF8.GetBytes("response body");
            _response.AddHeader("Content-Type", "text/plain");
            _reader = new StreamReader(_ioStream);
            _response.Send(new BinaryWriter(_ioStream));
            _ioStream.Seek(0, SeekOrigin.Begin);
        }

        [Fact]
        public void TestResponseContainsStatusLine() {
            SendToClientTestSetUp();

            Assert.Contains("HTTP/1.1 200 OK\r\n", _reader.ReadToEnd());
        }

        [Fact]
        public void TestResponseContainsHeader() {
            SendToClientTestSetUp();

            Assert.Contains("Content-Type: text/plain\r\n\r\n", _reader.ReadToEnd());
        }

        [Fact]
        public void TestResponseContainsBody() {
            SendToClientTestSetUp();

            Assert.Contains("response body", _reader.ReadToEnd());
        }
    }
} 