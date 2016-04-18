using System.Threading.Tasks;

namespace HTTPServer {
    public interface IListener {
        bool Listening();
        Task<IClientSocket> AcceptAsync();
        void Start();
    }
}
