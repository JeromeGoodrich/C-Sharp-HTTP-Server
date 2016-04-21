using System.Collections.Generic;

namespace HTTPServer {

    public class Handlers {
        public static List<IHandler> AllHandlers = new List<IHandler>() {
            new DirHandler(""),
            new BasicAuthHandler(),
        };
    }
}