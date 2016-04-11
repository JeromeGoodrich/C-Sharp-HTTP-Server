using HTTPServer;

namespace HTTPServerTest
{
    public class MockListener : IListener {
        private int _callCounter;
        private IClientSocket _mockSocket;

        public MockListener(IClientSocket mockSocket) {
            this._mockSocket = mockSocket;
        }

        public bool Listening() {
            if (_callCounter == 0) {
                _callCounter++;
                return true;
            }
            else {
                return false;
            }
        }

        public IClientSocket Accept() {
            return _mockSocket;
        }
    }
}