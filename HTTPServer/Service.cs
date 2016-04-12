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
        private readonly IParser _parser;
        

        public Service(IClientSocket socket, IParser parser, IHandler handler)
        {
            this._socket = socket;
            this._parser = parser;
            this._handler = handler;
        }

        public void Run() {
            var request = _parser.Parse(_socket.GetStream());
            var response = _handler.Handle(request);
            response.Send(_socket.GetStream());
            _socket.Close();

        }
    }
}

