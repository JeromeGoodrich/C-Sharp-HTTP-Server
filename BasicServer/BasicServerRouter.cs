using ServerClassLibrary;

namespace BasicServer {
    public class BasicServerRouter {
        public static Router Configure() {
            var router = new Router();
            router.AddRoute(new Route("GET", "/", new HelloWorldHandler()));
            router.AddRoute(new Route("GET", "/form", new BasicFormHandler()));
            router.AddRoute(new Route("POST", "/form", new BasicFormHandler()));
            return router;
        }
    }
}
