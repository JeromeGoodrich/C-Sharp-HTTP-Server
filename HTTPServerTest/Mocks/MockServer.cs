using HTTPServer;

namespace HTTPServerTest.Mocks {
    public class MockServer {
        private readonly IListener _listener;
        private readonly IServiceFactory _serviceFactory;

        public MockServer(IListener listener, IServiceFactory serviceFactory) {
            _listener = listener;
            _serviceFactory = serviceFactory;

        }

        public void Start() {
            var counter = 0;
            _listener.Start();
            while (counter != 0) {
                System.Console.WriteLine("Waiting...");
                var socket = _listener.Accept();
                System.Console.WriteLine("Accepted");
                var service = _serviceFactory.CreateService(socket);
                service.Run();
                counter++;
            }
        }

    }
}