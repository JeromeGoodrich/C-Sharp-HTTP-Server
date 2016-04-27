using System.Text;

namespace HTTPServer {
    public class ParamsHandler : IHandler {
        public IResponse Handle(Request request) {
            var response =  new Response(200, request.Version);
            var parameters = request.GetParameters();
            var formattedParams = "";
            foreach (var param in parameters) {
                var paramKey = param.Key;
                var paramValue = param.Value;
                formattedParams += paramKey + " = " + paramValue + " ";
            }
            response.Body = Encoding.UTF8.GetBytes(formattedParams);
            return response;
        }
    }
}