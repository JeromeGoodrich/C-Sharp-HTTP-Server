namespace HTTPServer {
    public interface IRouter {
        IHandler Route(Request request);
    }
}