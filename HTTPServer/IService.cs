namespace HTTPServer {
    public interface IService {

        void Run();
        bool IsRunning();
        IClientSocket GetSocket();
        void SetSocket(IClientSocket socket);
    }
}