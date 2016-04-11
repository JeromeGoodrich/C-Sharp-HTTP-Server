using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    public class Service : IService {
        private readonly IClientSocket _socket;
        private readonly IHandler _handler;

        public Service(IClientSocket socket, IHandler handler)
        {
            this._socket = socket;
            this._handler = handler;
        }

        public void Run() {
            var request = new Request(_socket.GetStream());
            var response = _handler.Handle(request);
            _socket.Close();
        }
    }
}
