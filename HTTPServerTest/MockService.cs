using HTTPServer;

namespace HTTPServerTest {
    public class MockService : IService {
        private bool _running = false;
        private IClientSocket _socket;


        public MockService() {
        }

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
            this._socket = socket;
        }
    }
}