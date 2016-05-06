using System;
using System.Collections;

namespace ServerClassLibrary {
    public class Router : IRouter {

        public ArrayList Routes = new ArrayList();

        public IHandler Route(Request request) {
            IHandler handler = null;
            foreach (Route route in Routes) {
                if (route.Path.Equals(request.Path) && route.Method.Equals(request.Method)) {
                    handler = route.Handler;
                    break;
                } else if (route.Path.Equals(request.Path) && !route.Method.Equals(request.Method)) {
                    handler = new MethodNotAllowedHandler();
                }
            }
            if (handler == null) {
                handler = new NotFoundHandler();
            }
            return handler;
        }

       

        public void AddRoute(Route route) {
            Routes.Add(route);
        }
    }
}