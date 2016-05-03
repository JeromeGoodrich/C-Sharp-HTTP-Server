namespace ServerClassLibrary {
    public interface IRequestProcessorFactor {
        IRequestProcessor CreateProcessor(IClientSocket socket);
    }
}