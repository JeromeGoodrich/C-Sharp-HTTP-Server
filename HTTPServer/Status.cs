using System.Collections.Generic;

namespace HTTPServer {
    public static class Status {
        public static Dictionary<int, string> StatusDictionary
            = new Dictionary<int, string> {
                {200, "OK"},
            };
    }
}