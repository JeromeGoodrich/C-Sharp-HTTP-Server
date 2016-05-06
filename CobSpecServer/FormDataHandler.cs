using System.Text;
using ServerClassLibrary;

namespace CobSpecServer {
    public class FormDataHandler : IHandler {
        private static byte[] _formData;

        public IResponse Handle(Request request) {
            var response = new Response(200, request.Version);
            if (request.Method.Equals("GET")) { 
                if (_formData != null) {
                    response.Body = _formData;
                } else {
                    return response;
                }
            } else if (request.Method.Equals("DELETE")) {
                _formData = null;
            }
            else {
                _formData = Encoding.UTF8.GetBytes(request.Body);
            }
            return response;
        }
    }
}