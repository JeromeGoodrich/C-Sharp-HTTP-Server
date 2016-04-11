using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPServer;

namespace HTTPServer {
    class Program {
        static void Main(string[] args) {
            var config = new ServerConfig();
            config.SetUp(args);
            var port = config.GetPort();
            var publicDir = config.GetPublicDir();
            //IListener listener = new Listener(port);
            //var server = new Server(listener, serviceFactory);
        }
    }
}
