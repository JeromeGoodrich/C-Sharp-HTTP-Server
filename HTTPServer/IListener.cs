using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer {
    public interface IListener {
        bool Listening();
        IClientSocket Accept();
    }
}
