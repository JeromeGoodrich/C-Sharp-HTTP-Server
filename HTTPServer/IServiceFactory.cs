namespace HTTPServer {
    public interface IServiceFactory {
        IService CreateService(IClientSocket socket);
    }
}