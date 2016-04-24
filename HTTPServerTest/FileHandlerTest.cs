using System;
using System.IO;
using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class FileHandlerTest {
        private readonly string _publicDir = Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\HTTPServerTest\Fixtures\");
        private readonly Request _request;
        private readonly FileHandler _handler;

        public FileHandlerTest() {
            _request = new Request()
            {
                Method = "GET",
                Version = "HTTP/1.1."
            };
            _handler = new FileHandler(_publicDir);
        }

        [Fact]
        public void WillHandleTest() {
            _request.Path = "/file1";

            Assert.True(_handler.WillHandle(_request.Method, _request.Path));
        }

        [Fact]
        public void WillNotHandleTest() {
            _request.Path = "/badpath";
            var handler = new FileHandler(_publicDir);

            Assert.False(handler.WillHandle(_request.Method, _request.Path));
        }

        [Fact]
        public void RequestForFileReturns200Test() {
            _request.Path = "/file1";
            var response = _handler.Handle(_request);

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void RequestForTextReturnsCorrectHeadersTest() { 
            _request.Path = "/file1";
            var response = _handler.Handle(_request);

            Assert.Equal("14", response.GetHeader("Content-Length"));
            Assert.Equal("text/plain", response.GetHeader("Content-Type"));
        }


        [Fact]
        public void ContentofTextFileIsInResponseBodyTest() {
            _request.Path = "/file1";
            var response = _handler.Handle(_request);

            Assert.Equal("file1 contents", Encoding.UTF8.GetString(response.Body));
        }

        [Fact]
        public void RequestForImageReturnsCorrectHeadersTest() { 
            _request.Path = "/image.jpeg";
            var response = _handler.Handle(_request);

            Assert.Equal("157751", response.GetHeader("Content-Length"));
            Assert.Equal("image/jpeg", response.GetHeader("Content-Type"));
        }

        [Fact]
        public void RequestForImageHasResponseBodyTest()
        {
            _request.Path = "/image.jpeg";
            var response = _handler.Handle(_request);

            Assert.NotNull(Encoding.UTF8.GetString(response.Body));
        }

        [Fact]
        public void RequestForSmallPdfReturnsCorrectHeadersTest() {
            _request.Path = "/pdf-sample.pdf";
            var response = _handler.Handle(_request);

            Assert.Equal("7945", response.GetHeader("Content-Length"));
            Assert.Equal("application/pdf", response.GetHeader("Content-Type"));
        }

        [Fact]
        public void RequestForBigPdfReturnsCorrectHeadersTest() {
            _request.Path = "/big-pdf.pdf";
            var response = _handler.Handle(_request);

            Assert.Equal("10762150", response.GetHeader("Content-Length"));
            Assert.Equal("application/pdf", response.GetHeader("Content-Type"));
            Assert.Contains("attachment;", response.GetHeader("Content-Disposition"));
        }

        [Fact]
        public void RequestForPartialContentReturns206Test() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=0-4");
            var response = _handler.Handle(_request);

            Assert.Equal(206, response.StatusCode);
        }


        private byte[] copyOfRange(byte[] src, int start, int end)
        {
            int len = end - start;
            byte[] dest = new byte[len];
            Array.Copy(src, start, dest, 0, len);
            return dest;
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithFullRangeTest() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=0-4");
            byte[] bytes = File.ReadAllBytes(_publicDir + _request.Path);

            var response = _handler.Handle(_request);

            Assert.Equal(copyOfRange(bytes, 0, 4), response.Body);
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithEndRangeTest()
        {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=-6");
            byte[] bytes = File.ReadAllBytes(_publicDir + _request.Path);
            var response = _handler.Handle(_request);

            Assert.Equal(copyOfRange(bytes, bytes.Length - 6, bytes.Length), response.Body);
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithStartRangeTest() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=4-");
            byte[] bytes = File.ReadAllBytes(_publicDir + _request.Path);
            var response = _handler.Handle(_request);

            Assert.Equal(copyOfRange(bytes, 4, bytes.Length), response.Body);
        }
    }
}