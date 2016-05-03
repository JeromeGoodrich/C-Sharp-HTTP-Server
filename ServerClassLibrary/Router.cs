using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ServerClassLibrary {
    public class Router : IRouter {

        private readonly string _publicDir;
        private readonly Dictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>();

        public Router(string publicDir, FileLogger logger) {
            _publicDir = publicDir;
        }

        
        public IHandler Route(Request request) {
            foreach (Route route in Routes ) {
                if (!route.Path.Equals(request.Path)) {
                    return new NotFoundHandler();
                } else if (route.Path.Equals(request.Path) && !route.Method.Equals(request.Method)) {
                    return new MethodNotAllowedHandler();
                } else {
                    return route.Handler;
                }
            }
            return null;
        }

        public ArrayList Routes = new ArrayList();

        public void AddRoute(Route route) {
            Routes.Add(route);
        }
    }
}