using System.IO;
using System.Linq;
using System.Text;
using ServerClassLibrary;

namespace CobSpecServer {
    public class DirHandler : IHandler {
        private readonly string _publicDir;

        public DirHandler(string publicDir) {
            _publicDir = publicDir;
        }

        public IResponse Handle(Request request) {
            var response = new Response(200, request.Version);
            byte[] body;
            var content = new ContentFactory().Build(request.GetHeader("Accept"), _publicDir);
            body = GenContent(content);
            response.AddHeader("Content-Type", content.GetContentType());
            response.AddHeader("Content-Length", body.Length.ToString());
            response.Body = body;
            return response;
        }

        private byte[] GenContent(IContent content) {
            string body = content.Generate();
            return Encoding.UTF8.GetBytes(body);
        }

        
    }
}