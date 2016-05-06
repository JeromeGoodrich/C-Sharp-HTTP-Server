using ServerClassLibrary;
using System.IO;

namespace CobSpecServer
{
    public class CobSpecRouter {
        public static Router Configure(string publicDir, string logFile) {
            var router = new Router();
            router.AddRoute(new Route("GET", "/logs", new BasicAuthHandler(logFile)));
            router.AddRoute(new Route("GET", "/", new DirHandler(publicDir)));
            router.AddRoute(new Route("GET", "/parameters", new ParamsHandler()));
            router.AddRoute(new Route("GET", "/form", new FormDataHandler()));
            router.AddRoute(new Route("POST", "/form", new FormDataHandler()));
            router.AddRoute(new Route("DELETE", "/form", new FormDataHandler()));
            router.AddRoute(new Route("PUT", "/form", new FormDataHandler()));
            router.AddRoute(new Route("GET", "/redirect", new RedirectHandler()));
            router.AddRoute(new Route("OPTIONS", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("GET", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("PUT", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("POST", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("HEAD", "/method_options", new OptionsHandler()));
            router.AddRoute(new Route("PATCH", "/patch-content.txt", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/big-pdf.pdf", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/file1", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/text-file.txt", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/file2", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/image.jpeg", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/image.gif", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/image.png", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/partial_content.txt", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/patch-content.txt", new FileHandler(publicDir)));
            router.AddRoute(new Route("GET", "/pdf-sample.pdf", new FileHandler(publicDir)));
            return router;
        }
    }
}
