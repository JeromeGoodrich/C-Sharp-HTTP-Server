using System.Collections;
using System.Collections.Generic;

namespace ServerClassLibrary {
    public class Router : IRouter {

        private readonly string _publicDir;
        private readonly Dictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>();

        public Router(string publicDir, FileLogger logger) {
            _publicDir = publicDir;
        }

        public ArrayList Routes = new ArrayList();
        
        public IHandler Route(Request request) {
            IHandler handler = null;
            foreach (Route route in Routes) {
                if (!route.Path.Equals(request.Path)) {
                    handler = new NotFoundHandler();
                } else if (route.Path.Equals(request.Path) && !route.Method.Equals(request.Method)) {
                    handler = new MethodNotAllowedHandler();
                }
                else {
                    handler = route.Handler;
                }
            }
            return handler;
        }

       

        public void AddRoute(Route route) {
            Routes.Add(route);
        }
    }
}