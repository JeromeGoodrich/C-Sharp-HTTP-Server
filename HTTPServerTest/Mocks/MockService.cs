using System.Threading;
using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockService : IService {
        private bool _running;
        public IClientSocket Socket { get; set; }

        public void Run() {
            _running = true;
        }

        public bool IsRunning() {
            return _running;
        }
    }
}