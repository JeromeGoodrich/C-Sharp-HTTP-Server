using HTTPServer;

namespace HTTPServerTest {
    public class FakeHandler : IHandler {
        private readonly string _fakePath;
        private readonly int _statusCode;

        public FakeHandler(string path, int statusCode) {
            _fakePath = path;
            _statusCode = statusCode;
        }

        public IResponse Handle(Request request) {
            return new Response(_statusCode, request.Version);
        }
    }
}