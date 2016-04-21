using System;
using System.IO;
using System.Text;

namespace HTTPServer {
    public class Service : IService {
        private readonly IClientSocket _socket;
        private readonly IHandler _handler;
        private readonly IParser _parser;
       

        public Service(IClientSocket socket, IParser parser, IHandler handler)  {
            _socket = socket;
            _parser = parser;
            _handler = handler;
        }

        public void Run() {
            using (var stream = _socket.GetStream()) {
                var reader = new StreamReader(stream);
                var writer = new BinaryWriter(stream);
                var request = _parser.Parse(reader);
                var response = _handler.Handle(request);
                response.Send(writer);

            }
            _socket.Close();
        }
    }
}

