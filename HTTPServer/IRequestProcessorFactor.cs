namespace HTTPServer {
    public interface IRequestProcessorFactor {
        IRequestProcessor CreateProcessor(IClientSocket socket);
    }
}