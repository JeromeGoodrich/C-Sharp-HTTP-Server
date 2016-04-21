using System.IO;

namespace HTTPServer {
    public class FileHandler : IHandler {
        private readonly string _publicDir;

        public FileHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            throw new System.NotImplementedException();
        }

        public bool WillHandle(string method, string path) {
            return File.Exists(_publicDir + path);
        }
    }
}