using ServerClassLibrary;

namespace ServerClassLibraryTest.Mocks {
    public class MockRequestProcessor : IRequestProcessor {
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