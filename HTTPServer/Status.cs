using System.Collections.Generic;

namespace HTTPServer {
    public class Status {
        public static Dictionary<int, string> StatusDictionary
            = new Dictionary<int, string> {
                {200, "OK"},
                {206, "Partial Content"},
                {401, "Unauthorized"},
                {404, "Not Found"},
                {405, "Method Not Allowed" }
            };
    }
}