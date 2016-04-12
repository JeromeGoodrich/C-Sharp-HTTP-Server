namespace HTTPServer {
    public interface IHandler {
        int GetCallsToHandle();
        IResponse Handle(Request request);
    }
}