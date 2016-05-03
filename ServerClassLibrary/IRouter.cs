namespace ServerClassLibrary {
    public interface IRouter {
        IHandler Route(Request request);
    }
}