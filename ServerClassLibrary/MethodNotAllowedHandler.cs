using System;

namespace ServerClassLibrary {
    public class MethodNotAllowedHandler : IHandler
    {
        public IResponse Handle(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
