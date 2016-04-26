using System.Threading.Tasks;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockListener : IListener {
        private readonly IClientSocket _mockSocket;
        private int _callCounter;
        private bool _listening;

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
            _listening = true;
        }

        public bool IsListening() {
            return _listening;
        }
    }
}