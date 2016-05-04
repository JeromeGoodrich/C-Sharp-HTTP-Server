using System.IO;

namespace CobSpecServer {
    public class JSONContent : IContent {
        public string _type;
        private string _dirName;

        public JSONContent(string acceptType, string dirName)
        {
            _type = acceptType;
            _dirName = dirName;
        }

        public string Generate() {
            const string jsonBoilerPlate = "{ files : [";
            var jsonFilesListing = GenFileListingJson(_dirName);
            const string jsonBoilerPlateEnd = "] }";
            return jsonBoilerPlate + jsonFilesListing + jsonBoilerPlateEnd;
        }

        public string GetContentType() {
            return _type;
        }

        private string GenFileListingJson(string dirName) {
            var files = Directory.GetFiles(dirName);
            var jsonFilesString = "";
            for (var i = 0; i < files.Length; i++) {
                var fileIndex = files[i].Split(Path.DirectorySeparatorChar).Length - 1;
                var fileName = files[i].Split(Path.DirectorySeparatorChar)[fileIndex];
                if (i == files.Length - 1) {
                    jsonFilesString += fileName;
                } else {
                    jsonFilesString += fileName + ", ";
                }
            }
            return jsonFilesString;
        }
    }
}
