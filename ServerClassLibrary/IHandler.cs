namespace ServerClassLibrary {
    public interface IHandler {
        IResponse Handle(Request request);
    }
}