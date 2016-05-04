using System;
using System.IO;
using System.Text;
using ServerClassLibrary;
using CobSpecServer;
using Xunit;

namespace CobSpecServerTest {
    public class FileHandlerTest {
        private readonly string _publicDir = Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures");

        private readonly Request _request;
        private readonly FileHandler _handler;
    
        public FileHandlerTest() {
            _request = new Request() {
                Method = "GET",
                Version = "HTTP/1.1."
            };
            _handler = new FileHandler(_publicDir);
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
        public void RequestForImageHasResponseBodyTest() {
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


        private byte[] CopyOfRange(byte[] src, int start, int end) {
            var length = end - start;
            var destinationArray = new byte[length];
            Array.Copy(src, start, destinationArray, 0, length);
            return destinationArray;
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithFullRangeTest() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=0-4");
            var bytes = File.ReadAllBytes(_publicDir + _request.Path);

            var response = _handler.Handle(_request);

            Assert.Equal(CopyOfRange(bytes, 0, 5), response.Body);
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithEndRangeTest() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=-6");
            var bytes = File.ReadAllBytes(_publicDir + _request.Path);
            var response = _handler.Handle(_request);

            Assert.Equal(CopyOfRange(bytes, (bytes.Length-1) - 6, bytes.Length-1), response.Body);
        }

        [Fact]
        public void ResponseBodyHasPartialContentWithStartRangeTest() {
            _request.Path = "/partial_content.txt";
            _request.AddHeader("Range", "bytes=4-");
            var bytes = File.ReadAllBytes(_publicDir + _request.Path);
            var response = _handler.Handle(_request);

            Assert.Equal(CopyOfRange(bytes, 4, bytes.Length-1), response.Body);
        }

        [Fact]
        public void ReturnMethodNotAllowedForUnsupportedMethod() {
            _request.Method = "PUT";
            _request.Path = "/file1";

            var response = _handler.Handle(_request);

            Assert.Equal(405, response.StatusCode);
        }

        [Fact]
        public void ReturnMethodNotAllowedForBogusRequest() {
            _request.Method = "bogusRequest";
            _request.Path = "/file1";

            var response = _handler.Handle(_request);

            Assert.Equal(405, response.StatusCode);
        }

        [Fact]
        public void PatchRequestHasCorrectHeaderAndStatusCode() {
            _request.Method = "PATCH";
            _request.Path = "/patch-content.txt";
            _request.AddHeader("If-Match", "1");
            _request.Body = "patched content";

            var response = _handler.Handle(_request);

            Assert.Equal(204, response.StatusCode);
            Assert.Equal("1", response.GetHeader("ETag"));
        }

        [Fact]
        public void PatchedContentReflectedInFile() {
            _request.Method = "GET";
            _request.Path = "/patch-content.txt";

            var response = _handler.Handle(_request);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("patched content", Encoding.UTF8.GetString(response.Body));
            PatchBackToDefault();
        }

        public void PatchBackToDefault() {
            _request.Method = "PATCH";
            _request.Path = "/patch-content.txt";
            _request.AddHeader("If-Match", "2");
            _request.Body = "default content";

            _handler.Handle(_request);
            var getRequest = new Request() {
                Method = "GET",
                Version = "HTTP/1.1",
                Path = "/patch-content.txt",
            };
            _handler.Handle(getRequest);
        }
    }
}