namespace ServerClassLibrary {
    public interface IListener {
        IClientSocket Accept();
        void Start();
    }
}