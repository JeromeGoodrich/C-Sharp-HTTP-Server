using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockService : IService {
        private bool _running;
        private IClientSocket _socket;

        public void Run() {
            _running = true;
        }

        public bool IsRunning() {
            return _running;
        }

        public IClientSocket GetSocket() {
           return _socket;
        }

        public void SetSocket(IClientSocket socket) {
            _socket = socket;
        }
    }
}