using System.IO;
using System.Text;

namespace HTTPServer {
    public class DirHandler : IHandler {
        public IResponse Handle(Request request) {
            var response = new Response(200, request.GetVersion());
            var body = GenHtmlBody(request);
            response.AddBody(body);
            return response;
        }

        private byte[] GenHtmlBody(Request request) {
            var htmlBoilerPlateStart = "<!Doctype html>\n" +
                                       "<html>\n" +
                                       "<head>\n</head>\n" +
                                       "<body>\n" +
                                       "<ol>\n";
            var subDirListing = GenDirListingHtml(request);
            var filesListing = GenFileListingHtml(request);
            var htmlBoilerPlateEnd = "</ol>\n" +
                                     "<body>\n" +
                                     "</html>";
            var htmlBody = htmlBoilerPlateStart + subDirListing + 
                           filesListing + htmlBoilerPlateEnd;
            return Encoding.UTF8.GetBytes(htmlBody);
        }

        private string GenFileListingHtml(Request request) {
            var files = Directory.GetFiles(request.GetPath());
            var filesList = "";
            foreach (var file in files)
            {
                filesList += "<li><a href=\"" + file + "\">" + file + "</a></li>\n";
            }
            return filesList;
        }

        private string GenDirListingHtml(Request request) {
            var subDirs = Directory.GetDirectories(request.GetPath());
            var subDirList = "";
            foreach (var dir in subDirs) {
                subDirList += "<li><a href=\"" + dir + "\">" + dir + "</a></li>\n";
            }
            return subDirList;
        }
    }
}