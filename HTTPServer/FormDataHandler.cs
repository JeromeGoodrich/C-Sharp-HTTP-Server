using System.Text;

namespace HTTPServer {
    public class FormDataHandler : IHandler {
        private byte[] _formData;

        public IResponse Handle(Request request) {
            if (request.Method.Equals("GET")) {
                var response = new Response(200, request.Version) {
                    Body = _formData
                };
                return response;
            }
            _formData = Encoding.UTF8.GetBytes(request.Body);
            return new Response(200, request.Version);
        }
    }
}