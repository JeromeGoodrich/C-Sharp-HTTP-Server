using System;
using System.IO;
using System.Linq;

namespace CobSpecServer {
    public class HTMLContent : IContent {

        private string _type;
        private string _dirName; 

        public HTMLContent(string acceptType, string dirName)
        {
            _type = acceptType;
            _dirName = dirName;
        }

        public string Generate() {
            const string htmlBoilerPlateStart = "<!DOCTYPE html>\n" +
                                                "<html>\n" +
                                                "<head>\n</head>\n" +
                                                "<body>\n" +
                                                "<ol>\n";
            var filesListing = GenFileListingHtml(_dirName);
            const string htmlBoilerPlateEnd = "</ol>\n" +
                                              "<body>\n" +
                                              "</html>";
            return htmlBoilerPlateStart + filesListing + htmlBoilerPlateEnd;
        }

        private string GenFileListingHtml(string dirName) {
            var files = Directory.GetFiles(dirName);
            return (from file in files let fileIndex = file.Split(Path.DirectorySeparatorChar).Length - 1
                    select file.Split(Path.DirectorySeparatorChar)[fileIndex]).Aggregate("", (current, fileName) 
                    => current + ("<li><a href=\"" + "/" + fileName + "\">" + fileName + "</a></li>\n"));
        }

        public string GetContentType() {
            return _type;
        }
    }
}
