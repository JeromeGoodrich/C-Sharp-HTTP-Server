namespace HTTPServer {
    public interface IHandler {
        IResponse Handle(Request request);
        bool WillHandle(string method, string path);
    }
}