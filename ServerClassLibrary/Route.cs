namespace ServerClassLibrary
{
    public class Route {

        public string Method { get; }
        public string Path { get; }
        public IHandler Handler { get; }

        public Route(string method, string path, IHandler handler){
            Method = method;
            Path = path;
            Handler = handler;
        }
    }
}
