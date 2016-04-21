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
            var response = new Response(200, request.Version);
            byte[] body;
            if (request.GetHeaders().ContainsKey("Accept") && request.GetHeader("Accept").Equals("application/json")) {
                body = GenJsonContent(dirName);
                response.AddHeader("Content-Type", "application/json");
                response.AddHeader("Content-Length", body.Length.ToString());
            }
            else {
                body = GenHtmlBody(dirName);
                response.AddHeader("Content-Length", body.Length.ToString());
            }
            response.Body = body;
            return response;
        }

        public bool WillHandle(string method, string path) {
            throw new NotImplementedException();
        }

        private byte[] GenJsonContent(string dirName) {
            const string jsonBoilerPlate = "{ files : [";
            var jsonFilesListing = GenFileListingJson(dirName);
            const string jsonBoilerPlateEnd = "] }";
            var jsonBody = jsonBoilerPlate + jsonFilesListing + jsonBoilerPlateEnd;
            return Encoding.UTF8.GetBytes(jsonBody);
        }

        private string GenFileListingJson(string dirName) {
            var files = Directory.GetFiles(dirName);
            var jsonFilesString = "";
            for (var i = 0; i < files.Length; i++) {
                var fileIndex = files[i].Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = files[i].Split(Path.DirectorySeparatorChar)[fileIndex];
                if (i == files.Length - 1) {
                    jsonFilesString += fileName;
                }
                else {
                    jsonFilesString += fileName + ", ";
                }
            }

            return jsonFilesString;
        }

        private byte[] GenHtmlBody(string dirName) {
            const string htmlBoilerPlateStart = "<!Doctype html>\n" +
                                                "<html>\n" +
                                                "<head>\n</head>\n" +
                                                "<body>\n" +
                                                "<ol>\n";
            var filesListing = GenFileListingHtml(dirName);
            const string htmlBoilerPlateEnd = "</ol>\n" +
                                              "<body>\n" +
                                              "</html>";
            var htmlBody = htmlBoilerPlateStart + filesListing + htmlBoilerPlateEnd;
            return Encoding.UTF8.GetBytes(htmlBody);
        }

        private string GenFileListingHtml(string dirName) {
            var files = Directory.GetFiles(dirName);
            var filesList = "";
            foreach (var file in files) {
                Console.WriteLine("Separator: " + Path.DirectorySeparatorChar);
                var fileIndex = file.Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = file.Split(Path.DirectorySeparatorChar)[fileIndex];
                filesList += "<li><a href=\"" + "/" + fileName + "\">" + fileName + "</a></li>\n";
            }
            return filesList;
        }
    }
}