using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer {
    public class ServiceFactory : IServiceFactory {
        private readonly IParser _parser;
        private readonly IHandler _handler;

        
        public ServiceFactory(IParser parser, IHandler handler) {
            this._parser = parser;
            this._handler = handler;
        }

        public IService CreateService(IClientSocket socket) {
            return new Service(socket, _parser, _handler);
        }
    }
}
