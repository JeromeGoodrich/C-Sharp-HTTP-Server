using System.IO;

namespace ServerClassLibrary {
    public class RequestProcessor : IRequestProcessor {
        private readonly IRouter _router;
        private readonly IParser _parser;
        private readonly IClientSocket _socket;

        public RequestProcessor(IClientSocket socket, IParser parser, IRouter router) {
            _socket = socket;
            _parser = parser;
            _router = router;
        }

        public void Run() {
            using (var stream = _socket.GetStream()) {
                var reader = new StreamReader(stream);
                var writer = new BinaryWriter(stream);
                var request = _parser.Parse(reader);
                var handler = _router.Route(request);
                var response = handler.Handle(request);
                response.Send(writer);
            }
            _socket.Close();
        }
    }
}