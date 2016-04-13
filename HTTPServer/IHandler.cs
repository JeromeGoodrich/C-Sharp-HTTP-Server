namespace HTTPServer {
    public interface IHandler { 
        IResponse Handle(Request request);
    }
}