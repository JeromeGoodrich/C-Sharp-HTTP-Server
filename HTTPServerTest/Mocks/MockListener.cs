using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockListener : IListener {
        private int _callCounter;
        private readonly IClientSocket _mockSocket;

        public MockListener(IClientSocket mockSocket) {
            _mockSocket = mockSocket;
        }

        public bool Listening() {
            if (_callCounter != 0) return false;
            _callCounter++;
            return true;
        }

        public IClientSocket Accept() {
            return _mockSocket;
        }

        public void Start() {
            throw new System.NotImplementedException();
        }
    }
}