using System.Collections.Generic;

namespace HTTPServer {

    public class Handlers {

        private static string _publicDir;

        public Handlers(string publicDir) {
            _publicDir = publicDir;
        }

        public List<IHandler> AllHandlers = new List<IHandler>() {
            new DirHandler(_publicDir),
            new BasicAuthHandler(),
        };
        

        
    }
}