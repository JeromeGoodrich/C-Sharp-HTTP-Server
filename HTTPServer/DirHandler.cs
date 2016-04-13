using System;
using System.IO;
using System.Text;

namespace HTTPServer {
    public class DirHandler : IHandler {
        private readonly string _publicDir;

        public DirHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            var dirName = _publicDir;
            var response = new Response(200, request.GetVersion());
            var body = GenHtmlBody(dirName);
            response.AddBody(body);
            return response;
        }

        private byte[] GenHtmlBody(string dirName) {
            var htmlBoilerPlateStart = "<!Doctype html>\n" +
                                       "<html>\n" +
                                       "<head>\n</head>\n" +
                                       "<body>\n" +
                                       "<ol>\n";
            var filesListing = GenFileListingHtml(dirName);
            var htmlBoilerPlateEnd = "</ol>\n" +
                                     "<body>\n" +
                                     "</html>";
            var htmlBody = htmlBoilerPlateStart + filesListing + htmlBoilerPlateEnd;
            return Encoding.UTF8.GetBytes(htmlBody);
        }

        private string GenFileListingHtml(string dirName) {
            var files = Directory.GetFiles(dirName);
            var filesList = "";
            foreach (var file in files) {
                var fileIndex = file.Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = file.Split(Path.DirectorySeparatorChar)[fileIndex];
                filesList += "<li><a href=\"" + "/" + fileName + "\">" + fileName + "</a></li>\n";
            }
            return filesList;
        }

       
    }
}