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
            var request = _parser.Parse(_socket.GetStream());
            var response = _handler.Handle(request);
            response.Send(_socket.GetStream());
            _socket.Close();
        }
    }
}

