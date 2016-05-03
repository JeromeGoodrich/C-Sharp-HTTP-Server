using System.Text;
using ServerClassLibrary;

namespace CobSpecServer {
    public class FormDataHandler : IHandler {
        private byte[] _formData;

        public IResponse Handle(Request request) {
            if (request.Method.Equals("GET")) {
                if (_formData == null) return new Response(200, request.Version);
                var response = new Response(200, request.Version) {
                    Body = _formData
                };
                return response;
            }
            if (request.Method.Equals("DELETE")) {
                _formData = null;
                return new Response(200, request.Version);
            }
            _formData = Encoding.UTF8.GetBytes(request.Body);
            return new Response(200, request.Version);
        }
    }
}