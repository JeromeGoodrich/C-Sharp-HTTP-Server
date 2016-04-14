namespace HTTPServer
{
    internal class Listener : IListener {
        private int _port;

        public Listener(int port) {
            _port = port;
        }

        public bool Listening() {
            throw new System.NotImplementedException();
        }

        public IClientSocket Accept() {
            throw new System.NotImplementedException();
        }
    }
}