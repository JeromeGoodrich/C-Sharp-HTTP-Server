namespace ServerClassLibrary {
    public interface IListener {
        bool Listening();
        IClientSocket Accept();
        void Start();
    }
}